namespace FluentPipelines;


/// <summary>
/// A Pipeline only exposing its first and last elements that can be appended to another or have another appended to it. Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_Open<T1, T3>(IPipe<T1> PipelineStart, IPipeOut<T3> Last) : IPipeline_Open<T1, T3>, IAsPipeline<Pipeline_Open<T1, T3>>, IAsPipeline<IPipeline_Open<T1, T3>>
{
   public Pipeline_Open(IPipe<T1, T3> p) : this(p, p) { }

   Pipeline_Open<T1, T3> IAsPipeline<Pipeline_Open<T1, T3>>.AsPipeline => this;
   IPipeline_Open<T1, T3> IAsPipeline<IPipeline_Open<T1, T3>>.AsPipeline => this;

   
}

/// <summary>
/// A Pipeline only exposing its first and last elements. Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_LeftSealed<TOut>(INoInputStartPipe PipelineStart, IPipeOut<TOut> Last) : IPipeline_LeftSealed, IPipeline_RightOpen<TOut>, IAsPipeline<Pipeline_LeftSealed<TOut>>, IAsPipeline<IPipeline_LeftSealed>
{
   Pipeline_LeftSealed<TOut> IAsPipeline<Pipeline_LeftSealed<TOut>>.AsPipeline => this;
   IPipeline_LeftSealed IAsPipeline<IPipeline_LeftSealed>.AsPipeline => this;

}
/// <summary>
/// A Pipeline only exposing its first element (which takes an input). Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_RightSealed<TPipelineInput>(IPipe<TPipelineInput> PipelineStart) : IPipeline_LeftOpen<TPipelineInput>, IAsPipeline<Pipeline_RightSealed<TPipelineInput>>, IAsPipeline<IPipeline_LeftOpen<TPipelineInput>>
{
   Pipeline_RightSealed<TPipelineInput> IAsPipeline<Pipeline_RightSealed<TPipelineInput>>.AsPipeline => this;
   IPipeline_LeftOpen<TPipelineInput> IAsPipeline<IPipeline_LeftOpen<TPipelineInput>>.AsPipeline => this;

   public static implicit operator Pipeline_RightSealed<TPipelineInput>(Action<TPipelineInput> func) => new(new PipeEnd<TPipelineInput>(func));

   public static implicit operator Pipeline_RightSealed<TPipelineInput>(AsyncAction<TPipelineInput> func) => new(new PipeEnd<TPipelineInput>(func));

}

/// <summary>
/// A Pipeline only exposing its first element (which takes no input). Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_FullySealed(INoInputStartPipe PipelineStart) : IPipeline_LeftSealed, IAsPipeline<IPipeline_LeftSealed>, IAsPipeline<Pipeline_FullySealed>
{
   IPipeline_LeftSealed IAsPipeline<IPipeline_LeftSealed>.AsPipeline => this;
   Pipeline_FullySealed IAsPipeline<Pipeline_FullySealed>.AsPipeline => this;

   /// <summary>
   /// Runs the pipeline
   /// </summary>
   public Task Run() => PipelineStart.Run();
}
