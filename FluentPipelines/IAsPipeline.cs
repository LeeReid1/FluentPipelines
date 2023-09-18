namespace FluentPipelines;

/// <summary>
/// Can be converted or used as a pipeline
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAsPipeline<T>
{
   T AsPipeline { get; }
}

