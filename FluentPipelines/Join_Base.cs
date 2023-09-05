namespace FluentPipelines;

/// <summary>
/// Joins two input pipes into a single output
/// </summary>
internal abstract class Join_Base<TIn1, TIn2> : IPipelineComponent
{
   bool IPipelineComponent.IsJoin => true;

   readonly SemaphoreSlim semaphore = new(1);


   readonly JoinIntakePipe<TIn1> p1;
   readonly JoinIntakePipe<TIn2> p2;

   readonly bool[] intakeReady;
   protected TIn1? value1;
   protected TIn2? value2;



   protected Join_Base(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2, int noInputs)
   {
      intakeReady = new bool[noInputs];
      p1 = new(this, OnInput1);
      p2 = new(this, OnInput2);

      input1.AddListener(p1);
      input2.AddListener(p2);
   }


   protected async Task OnInput<T>(T val, int intakeIndex, Action<T> setValue, SharedExecutionSettings settings)
   {
      ThrowIfDoubleNotified(intakeReady[intakeIndex]);

      await semaphore.WaitAsync().ConfigureAwait(false);

      try
      {
         ThrowIfDoubleNotified(intakeReady[intakeIndex]);
         setValue(val);
         intakeReady[intakeIndex] = true;

         await RunIfAllReady(settings).ConfigureAwait(false);
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

   Task OnInput1(TIn1 val, SharedExecutionSettings settings) => OnInput(val, 0, val => value1 = val, settings);
   Task OnInput2(TIn2 val, SharedExecutionSettings settings) => OnInput(val, 1, val => value2 = val, settings);

   static void ThrowIfDoubleNotified(bool isReady)
   {
      if (isReady)
      {
         throw new InvalidOperationException("Pipeline busy, recursive, or in an error state");
      }
   }

   async Task RunIfAllReady(SharedExecutionSettings settings)
   {
      if (intakeReady.All(a=>a))
      {
         try
         {
            await Run(settings).ConfigureAwait(false);
         }
         finally
         {
            for (int i = 0; i < intakeReady.Length; i++)
            {
               intakeReady[i] = false;
            }
         }
      }
   }

   protected abstract Task Run(SharedExecutionSettings settings);
   public abstract IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents();
}
