namespace FluentPipelines;


/// <summary>
/// A Pipeline only exposing its first and last elements that can be appended to another or have another appended to it. Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_Open<T1, T3>(IPipe<T1> PipelineStart, IPipeOut<T3> Last) : IPipeline_LeftOpen<T1>, IPipeline_RightOpen<T3>
{

   public static implicit operator Pipeline_Open<T1, T3>(Func<T1, T3> func) => new Pipe<T1, T3>(func);

   public static implicit operator Pipeline_Open<T1, T3>(AsyncFunc<T1, T3> func) => new Pipe<T1, T3>(func);

   public static implicit operator Pipeline_Open<T1, T3>(Pipe<T1, T3> p) => new(p, p);
}

/// <summary>
/// A Pipeline only exposing its first and last elements. Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_LeftSealed<TOut>(INoInputStartPipe PipelineStart, IPipeOut<TOut> Last) : IPipeline_LeftSealed, IPipeline_RightOpen<TOut>;
/// <summary>
/// A Pipeline only exposing its first element (which takes an input). Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_RightSealed<TPipelineInput>(IPipe<TPipelineInput> PipelineStart) : IPipeline_LeftOpen<TPipelineInput>;

/// <summary>
/// A Pipeline only exposing its first element (which takes no input). Indended only for use within <see cref="ExtensionMethods"/>
/// </summary>
public readonly record struct Pipeline_FullySealed(INoInputStartPipe PipelineStart) : IPipeline_LeftSealed
{
   /// <summary>
   /// Runs the pipeline
   /// </summary>
   public Task Run() => PipelineStart.Run();
}
