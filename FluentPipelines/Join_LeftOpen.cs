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


/// <summary>
/// Helps join joins by converting two <see cref="DisposableTuple{T1, T2}"/> into a single <see cref="DisposableTuple{T1, T2, T3}"/>
/// </summary>
[Obsolete]
internal class JoinDeconstruct_2<TIn1, TIn2, TIn3> : Pipe<DisposableTuple<DisposableTuple<TIn1, TIn2>, DisposableTuple<TIn1, TIn3>>, DisposableTuple<TIn1, TIn2, TIn3>>
{
#warning cleanup - shouldbe unused
   public JoinDeconstruct_2() : base(Convert)
   {
   }

   static DisposableTuple<TIn1, TIn2, TIn3> Convert(DisposableTuple<DisposableTuple<TIn1, TIn2>, DisposableTuple<TIn1, TIn3>> inputs)
   {
      return new(inputs.Val1.Val1, inputs.Val1.Val2, inputs.Val2.Val2);
   }


   protected override bool InputIsOutput(DisposableTuple<DisposableTuple<TIn1, TIn2>, DisposableTuple<TIn1, TIn3>> input, DisposableTuple<TIn1, TIn2, TIn3> output)
   {
      // Output is just input rearranged
      return true;
   }

}