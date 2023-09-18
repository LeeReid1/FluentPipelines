using System.Diagnostics;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FluentPipelines;

/// <summary>
/// A pipeline stage that accepts input, provides it to a function, and passes the result to any listening <see cref="IPipeOut{TOut}"/>
/// </summary>
internal class Pipe<TIn, TOut> : IPipe<TIn,TOut>
{
   readonly List<IPipe<TOut>> subsequentPipes = new();
   readonly List<INoInputStartPipe> onErrorPipes = new();
   readonly AsyncFunc<TIn, TOut> func;
   readonly SemaphoreSlim semaphore = new(1);
   public string Name => func.Name;
   

   /// <summary>
   /// Overrides WarnIfResultUnused in execution settings
   /// </summary>
   protected virtual bool? WarnIfResultUnusedOverride => null;
   /// <summary>
   /// The number of pipes this sends results to
   /// </summary>
   protected virtual int NoSubsequentPipes => subsequentPipes.Count;

   Pipeline_Open<TIn, TOut>? asPipeline;
   Pipeline_Open<TIn, TOut> IAsPipeline<Pipeline_Open<TIn, TOut>>.AsPipeline => asPipeline ??= new(this);


   public Pipe(Func<TIn, TOut> func, string? name = null) : this(new AsyncFunc<TIn, TOut>(func, name))
   {
   }
   public Pipe(Func<TIn, Task<TOut>> func, string? name = null) : this(new AsyncFunc<TIn, TOut>(func, name))
   {
   }
   public Pipe(AsyncFunc<TIn, TOut> function) 
   {
      func = function;
   }



   internal Task Run(AutoDisposableValue<TIn> input) => Run(input, new SharedExecutionSettings());
   Task IPipe<TIn>.Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings) => this.Run(input, executionSettings);

   internal Task Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings)
   {
      return AsThreadsafe(Sub);

      async Task Sub()
      {
         TIn val = input.Value;
         (bool success, TOut? result) = await RunFunc(val).ConfigureAwait(false);

         CleanUp(input, success && InputIsOutput(val, result!));

         if (success)
         {
            PrintStatus("Completed", Verbosity.Normal);
            try
            {
               await RunSubsequent(result!, executionSettings, true).ConfigureAwait(false);
            }
            catch when (onErrorPipes.Count != 0)
            {
               await RunOnError(executionSettings).ConfigureAwait(false);
            }
         }
         else
         {
            await RunOnError(executionSettings).ConfigureAwait(false);
         }

         async Task<(bool, TOut?)> RunFunc(TIn val)
         {
            PrintStatus("Running", Verbosity.Minimal);

            try
            {

               return (true, await func.Invoke(val).ConfigureAwait(false));
            }
            catch (Exception e) when (onErrorPipes.Count != 0)
            {
               PrintStatus("Error: " + e.ToString(), Verbosity.Minimal);

               return (false, default);
            }
            // else allow the Exception to propogate beyond here
         }
         //
         // Eagerly Disposes objects as required
         //
         void CleanUp(AutoDisposableValue<TIn> input, bool outputIsInput)
         {
            PrintStatus("Cleaning Up", Verbosity.Verbose);
            if (outputIsInput)
            {
               // input was re-used as output
               // This can cause issues if a parallel pipe wanted to use it 
               // as input, because we have modified it either before or while
               // they were reading it. Assert that that's not the case
               Debug.Assert(!input.IsShared, $"Pipe {Name} outputs the same {typeof(TIn)} object as the input. This input is shared with other pipes. This can lead to unintended side effects including broken thread-safety, because other pipes will read from this object as or after it has been modified. Copy the input and modify that copy instead.");

               if (NoSubsequentPipes == 0 && !input.IsShared)
               {
                  // Nothing else appears to use this object
                  input.UseComplete();
               }
               else
               {
                  // We no longer take responsibility for cleaning up this resource
                  // The subsequent pipes will run this method and eventually lead to 
                  // the resource being cleaned up through another AutoDisposableValue
               }

            }
            else
            {
               // Notify the AutoDisposableValue we are done with
               // the input. If we are the last, it will eagerly
               // clean up any object that is IDisposable
               input.UseComplete();
            }
         }


         void PrintStatus(string status, Verbosity minVerbosityLevel)
         {
            if (Name.Length != 0 && executionSettings.LogVerbosity >= minVerbosityLevel)
            {
               executionSettings.Log($"{Name}: {status}");
            }
         }
      }
   }

   private async Task RunOnError(SharedExecutionSettings executionSettings)
   {
      foreach (var item in onErrorPipes)
      {
         await item.Run(executionSettings);
      }
   }

   protected virtual bool InputIsOutput(TIn input, TOut output) => object.ReferenceEquals(input, output);

   /// <summary>
   /// Passes the result to any listening IPipes. Also override <see cref="NoSubsequentPipes"/> if overriding this
   /// </summary>
   protected virtual async Task RunSubsequent(TOut result, SharedExecutionSettings executionSettings, bool disposeInput)
   {
      if (subsequentPipes.Count == 0)
      {
         // This pipe goes nowhere. Clean up the output ourselves
         if (disposeInput)
         {
            (result as IDisposable)?.Dispose();

            Debug.WriteLineIf(WarnIfResultUnusedOverride ?? executionSettings.WarnIfResultUnused, $"Pipe {Name} outputs a value but is not connected to other pipes, so the result is Disposed upon completion. Did you forget to add a save step that returns no value?");
         }
      }
      else
      {
         AutoDisposableValue<TOut> inputForSubsequent = new(disposeInput ? subsequentPipes.Count : int.MaxValue, result);

         foreach (var item in subsequentPipes)
         {
            await item.Run(inputForSubsequent, executionSettings);
         }
      }
   }

   

   Task IPipeOut<TOut>.AddListener(IPipe<TOut> pipe) => AsThreadsafe(() => subsequentPipes.Add(pipe));

   public Task AddOnErrorListener(INoInputStartPipe pipe) => AsThreadsafe(() => AddOnErrorListener_NotThreadsafe(pipe));
   private void AddOnErrorListener_NotThreadsafe(INoInputStartPipe pipe) => onErrorPipes.Add(pipe);
   public Task AddOnErrorListenerToEntireTree(INoInputStartPipe pipe)
   {
      return AsThreadsafe(Sub);

      async Task Sub()
      {
         AddOnErrorListener_NotThreadsafe(pipe); // need to use non-threadsafe to avoid a deadlock
         foreach (var item in PipelineComponentMethods.GetTree(this).Cast<IPipe>())
         {
            if (item != this)
            {
               await item.AddOnErrorListener(pipe).ConfigureAwait(false);
            }
         }
      }
   }

   /// <summary>
   /// Runs an action in a threadsafe manner
   /// </summary>
   private async Task AsThreadsafe(Action act)
   {
      await semaphore.WaitAsync().ConfigureAwait(false);
      try
      {
         act.Invoke();
      }
      finally
      {
         semaphore.Release();
      }

   }

   /// <summary>
   /// Runs an async Action in a threadsafe manner
   /// </summary>
   private async Task AsThreadsafe(Func<Task> act)
   {
      await semaphore.WaitAsync().ConfigureAwait(false);
      try
      {
         await act.Invoke().ConfigureAwait(false);
      }
      finally
      {
         semaphore.Release();
      }

   }

   public override string ToString() => Name.Length == 0 ? GetType().Name : Name;

   public static implicit operator Pipe<TIn, TOut>(AsyncFunc<TIn, TOut> func) => new(func); 
   public static implicit operator Pipe<TIn, TOut>(Func<TIn, Task<TOut>> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
   public static implicit operator Pipe<TIn, TOut>(Func<TIn, TOut> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK


   IEnumerable<IPipelineComponent> IPipelineComponent.GetImmediateDownstreamComponents() => this.GetImmediateDownstreamComponents();

   protected virtual IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
   {
      foreach (var item in subsequentPipes)
      {
         yield return item;
      }
   }
}
