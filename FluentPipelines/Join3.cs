namespace FluentPipelines;

/// <summary>
/// Joins the result of three pipelines
/// </summary>
internal abstract class Join3<TIn1, TIn2, TIn3> : Join_Base<TIn1, TIn2>
{
   readonly JoinIntakePipe<TIn3> p3;
   protected TIn3? value3;

   protected readonly Pipe<DisposableTuple<TIn1, TIn2, TIn3>, DisposableTuple<TIn1, TIn2, TIn3>> following = new(a=>a, "Join");
   
   public Join3(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2, IPipeOut<TIn3> input3) : base(input1, input2, 3)
   {

      p3 = new(this, OnInput3);

      input3.AddListener(p3);
   }

   Task OnInput3(TIn3 val, SharedExecutionSettings settings) => OnInput(val, 2, val => value3 = val, settings);

   protected override async Task Run(SharedExecutionSettings settings)
   {
      AutoDisposableValue<DisposableTuple<TIn1, TIn2, TIn3>> inputForSubsequent = new(1, new DisposableTuple<TIn1, TIn2, TIn3>(value1!, value2!, value3!));
      await following.Run(inputForSubsequent, settings).ConfigureAwait(false);
   }

   public override IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
   {
      yield return following;
   }
}
