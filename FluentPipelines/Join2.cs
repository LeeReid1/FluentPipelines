namespace FluentPipelines;

/// <summary>
/// Joins the results of two pipelines
/// </summary>
internal abstract class Join2<TIn1, TIn2> : Join_Base<TIn1, TIn2>
{
   protected readonly Pipe<DisposableTuple<TIn1, TIn2>, DisposableTuple<TIn1, TIn2>> following = new(a=>a, "Join");
   public Join2(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2) : base(input1, input2, 2)
   {

   }

   protected override async Task Run(SharedExecutionSettings settings)
   {
      AutoDisposableValue<DisposableTuple<TIn1, TIn2>> inputForSubsequent = new(1, new DisposableTuple<TIn1, TIn2>(value1!, value2!));
      await following.Run(inputForSubsequent, settings).ConfigureAwait(false);
   }

   public override IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
   {
      yield return following;
   }
}
