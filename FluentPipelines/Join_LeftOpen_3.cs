namespace FluentPipelines;

internal class Join_LeftOpen_3<TPipelineStart, TIn1, TIn2, TIn3> : Join3<TIn1, TIn2, TIn3>, IAsPipeline<Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2, TIn3>>>
{

   readonly Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2, TIn3>> _asPipeline;
   Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2, TIn3>> IAsPipeline<Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2, TIn3>>>.AsPipeline => _asPipeline;

   public Join_LeftOpen_3(IPipeline_Open<TPipelineStart, TIn1> input1, IPipeOut<TIn2> input2, IPipeOut<TIn3> input3) : this(input1.Last, input2, input3, input1.PipelineStart)
   {      
   }
   public Join_LeftOpen_3(IPipeOut<TIn1> input1, IPipeline_Open<TPipelineStart, TIn2> input2, IPipeOut<TIn3> input3) : this(input1, input2.Last, input3, input2.PipelineStart)
   {
   }
   
   public Join_LeftOpen_3(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2, IPipeOut<TIn3> input3, IPipe<TPipelineStart> start) : base(input1, input2, input3)
   {
      _asPipeline = new(start, following);
   }
}
