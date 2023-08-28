namespace FluentPipelines;

/// <summary>
/// Can be converted or used as a pipeline
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAsPipeline<T>
{
   T AsPipeline { get; }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TThenSource">The pipe that was fed into Then</typeparam>
/// <typeparam name="TFullPipeline">The result of Then which can be chained with another Then or similar operation</typeparam>
/// <param name="ThenSource">The pipe that was fed into Then</param>
/// <param name="Pipeline">The result of Then which can be chained with another Then or similar operation</param>
public readonly record struct ThenResult<TThenSource, TFullPipeline>(IPipeOut<TThenSource> ThenSource, TFullPipeline Pipeline) : IAsPipeline<TFullPipeline>
{
   TFullPipeline IAsPipeline<TFullPipeline>.AsPipeline => Pipeline;
}

