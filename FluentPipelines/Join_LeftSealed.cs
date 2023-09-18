namespace FluentPipelines;

internal class Join_LeftSealed<TIn1, TIn2> : Join2<TIn1,TIn2>, IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2>>>
{
   readonly Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2>> _asPipeline;
   Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2>> IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2>>>.AsPipeline => _asPipeline;
   public Join_LeftSealed(Pipeline_LeftSealed<TIn1> input1, IPipeOut<TIn2> input2) : base(input1.Last, input2)
   {
      _asPipeline = new(input1.PipelineStart, following);
   }
}
