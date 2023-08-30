namespace FluentPipelines;

/// <summary>
/// Joins two input pipes into a single output
/// </summary>
internal class Join<TIn1, TIn2> : IAsPipeline<IPipeline_RightOpen<DisposableTuple<TIn1, TIn2>>>, IPipelineComponent
{
   bool IPipelineComponent.IsJoin => true;
   /// <summary>
   /// Special pipe which uses a callback rather than expecting to hold a pipe
   /// </summary>
   /// <typeparam name="T"></typeparam>
   class IntakePipe<T> : Pipe<T,T>
   {
      readonly IPipelineComponent _parent;
      readonly Func<T, SharedExecutionSettings, Task> callback;
      public IntakePipe(IPipelineComponent parent, Func<T, SharedExecutionSettings, Task> callback) : base(a => a, string.Empty)
      {
         _parent = parent;
         this.callback = callback;
      }

      protected override IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
      {
         List<IPipelineComponent> l = new(base.GetImmediateDownstreamComponents())
         {
            _parent
         };
         return l;
      }

      protected override async Task RunSubsequent(T res, SharedExecutionSettings executionSettings, bool disposeInput)
      {
         await callback.Invoke(res, executionSettings).ConfigureAwait(false);
         await base.RunSubsequent(res, executionSettings, disposeInput).ConfigureAwait(false); // should have nothing to do
      }
   }

   readonly SemaphoreSlim semaphore = new(1);


   readonly IntakePipe<TIn1> p1;
   readonly IntakePipe<TIn2> p2;

   bool c1Ready, c2Ready;
   TIn1? value1;
   TIn2? value2;

   readonly Pipe<DisposableTuple<TIn1, TIn2>, DisposableTuple<TIn1, TIn2>> following2 = new(a=>a, "Join");


   Pipeline_Open<TIn1, DisposableTuple<TIn1, TIn2>>? _asPipeline;
   public IPipeline_RightOpen<DisposableTuple<TIn1, TIn2>> AsPipeline => _asPipeline ??= new Pipeline_Open<TIn1, DisposableTuple<TIn1, TIn2>>(p1, following2);

   public Join(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2)
   {
      p1 = new(this, OnInput1);
      p2 = new(this, OnInput2);

      input1.AddListener(p1);
      input2.AddListener(p2);

   }

   async Task OnInput1(TIn1 val, SharedExecutionSettings settings)
   {
      ThrowIfDoubleNotified(c1Ready);
      await semaphore.WaitAsync();

      try
      {
         ThrowIfDoubleNotified(c1Ready);
         value1 = val;
         c1Ready = true;

         await RunIfBothReady(settings);
      }
      catch (Exception e)
      {
         settings.ErrorCoordinator.OnError(e);
         throw;
      }
      finally
      {
         semaphore.Release();
      }

   }

   async Task OnInput2(TIn2 val, SharedExecutionSettings settings)
   {
      ThrowIfDoubleNotified(c2Ready);
      await semaphore.WaitAsync();

      try
      {
         ThrowIfDoubleNotified(c2Ready);
         value2 = val;
         c2Ready = true;

         await RunIfBothReady(settings);
      }
      catch (Exception e)
      {
         settings.ErrorCoordinator.OnError(e);
         throw;
      }
      finally
      {
         semaphore.Release();
      }

   }

   static void ThrowIfDoubleNotified(bool isReady)
   {
      if (isReady)
      {
         throw new InvalidOperationException("Pipeline busy, recursive, or in an error state");
      }
   }

   async Task RunIfBothReady(SharedExecutionSettings settings)
   {
      if (c1Ready && c2Ready)
      {
         try
         {
            AutoDisposableValue<DisposableTuple<TIn1, TIn2>> inputForSubsequent = new(1, new DisposableTuple<TIn1, TIn2>() { Val1 = value1!, Val2 = value2! });
            await following2.Run(inputForSubsequent, settings).ConfigureAwait(false);
         }
         finally
         {
            c1Ready = false;
            c2Ready = false;
         }
      }
   }

   public IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
   {
      yield return following2;
   }
}
