namespace FluentPipelines;

internal class Join_LeftSealed_3<TIn1, TIn2, TIn3> : Join3<TIn1,TIn2,TIn3>, IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2,TIn3>>>
{

   readonly Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2,TIn3>> _asPipeline;

   Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2,TIn3>> IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TIn1, TIn2,TIn3>>>.AsPipeline => _asPipeline;

   public Join_LeftSealed_3(Pipeline_LeftSealed<TIn1> input1, IPipeOut<TIn2> input2, IPipeOut<TIn3> input3) : base(input1.Last, input2, input3)
   {
      _asPipeline = new(input1.PipelineStart, following);
   }
}
