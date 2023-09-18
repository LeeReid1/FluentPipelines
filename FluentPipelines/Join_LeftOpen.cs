namespace FluentPipelines;

internal class Join_LeftOpen<TPipelineStart, TIn1, TIn2> : Join2<TIn1, TIn2>, IAsPipeline<Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2>>>
{
   readonly Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2>> _asPipeline;

   Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2>> IAsPipeline<Pipeline_Open<TPipelineStart, DisposableTuple<TIn1, TIn2>>>.AsPipeline => _asPipeline;

   public Join_LeftOpen(IPipeline_Open<TPipelineStart, TIn1> input1, IPipeOut<TIn2> input2) : this(input1.Last, input2, input1.PipelineStart)
   {  
   }

   public Join_LeftOpen(IPipeOut<TIn1> input1, IPipeline_Open<TPipelineStart, TIn2> input2) : this(input1, input2.Last, input2.PipelineStart)
   {
   }
   
   public Join_LeftOpen(IPipeOut<TIn1> input1, IPipeOut<TIn2> input2, IPipe<TPipelineStart> start) : base(input1, input2)
   {
      _asPipeline = new(start, following);
   }
}