using System.Diagnostics;
using System.Threading;

namespace FluentPipelines;

/// <summary>
/// A pipeline stage that accepts input, provides it to a function, and passes the result to any listening <see cref="IPipeOut{TOut}"/>
/// </summary>
internal class Pipe<TIn, TOut> : IPipe<TIn,TOut>
{
   readonly List<IPipe<TOut>> subsequentPipes = new();
   readonly AsyncFunc<TIn, TOut> func;
   readonly SemaphoreSlim semaphore = new(1);
   public string Name { get; }

   /// <summary>
   /// Overrides WarnIfResultUnused in execution settings
   /// </summary>
   protected virtual bool? WarnIfResultUnusedOverride => null;

   public Pipe(Func<TIn, TOut> func, string? name = null) : this(new AsyncFunc<TIn,TOut>(func, name))
   {
   }
   public Pipe(Func<TIn, Task<TOut>> func, string? name = null) : this(new AsyncFunc<TIn, TOut>(func, name))
   {
   }
   public Pipe(AsyncFunc<TIn, TOut> func)
   {
      this.func = func;
      Name = func.Name;
   }

   internal Task Run(AutoDisposableValue<TIn> input) => Run(input, new SharedExecutionSettings());
   Task IPipe<TIn>.Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings) => this.Run(input, executionSettings);

   internal Task Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings)
   {
      return AsThreadsafe(Sub);

      async Task Sub()
      {
         PrintStatus("Running", Verbosity.Minimal);
         TIn val = input.Value;
         TOut result = await func.Invoke(val);

         Cleanup(input, val, result);

         PrintStatus("Completed", Verbosity.Normal);
         await RunSubsequent(result, executionSettings, true);


         //
         // Eagerly Disposes objects as required
         //
         void Cleanup(AutoDisposableValue<TIn> input, TIn val, TOut result)
         {
            

            PrintStatus("Cleaning Up", Verbosity.Verbose);
            bool outputIsInput = object.ReferenceEquals(val, result);
            if (outputIsInput)
            {
               // input was re-used as output
               // This can cause issues if a parallel pipe wanted to use it 
               // as input, because we have modified it either before or while
               // they were reading it. Assert that that's not the case
               Debug.Assert(!input.IsShared, $"Pipe {Name} outputs the same {typeof(TIn)} object as the input. This input is shared with other pipes. This can lead to unintended side effects including broken thread-safety, because other pipes will read from this object as or after it has been modified. Copy the input and modify that copy instead.");


               if (subsequentPipes.Count == 0 && !input.IsShared)
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



   /// <summary>
   /// Passes the result to any listening IPipes
   /// </summary>
   protected async Task RunSubsequent(TOut result, SharedExecutionSettings executionSettings, bool disposeInput)
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

   /// <summary>
   /// Runs an action in a threadsafe manner
   /// </summary>
   private async Task AsThreadsafe(Action act)
   {
      await semaphore.WaitAsync();
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
      await semaphore.WaitAsync();
      try
      {
         await act.Invoke();
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

   /// <summary>
   /// Creates a pipeline with no input
   /// </summary>
   /// <returns></returns>
   public Pipeline_Open<TIn, TOut> ToPipeline() => new(this, this);


}

