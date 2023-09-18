namespace FluentPipelines;

/// <summary>
/// Results from a node linking function such as Then. Not intended to be used exept for other node linking functions
/// </summary>
/// <typeparam name="TThenSource">The pipe that was fed into Then</typeparam>
/// <typeparam name="TFullPipeline">The result of Then which can be chained with another Then or similar operation</typeparam>
/// <param name="ThenSource">The pipe that was fed into Then</param>
/// <param name="Pipeline">The result of Then which can be chained with another Then or similar operation</param>
public readonly record struct ThenResult<TThenSource, TFullPipeline>(IPipeOut<TThenSource> ThenSource, TFullPipeline Pipeline) : IAsPipeline<TFullPipeline>
{
   TFullPipeline IAsPipeline<TFullPipeline>.AsPipeline => Pipeline;
}

