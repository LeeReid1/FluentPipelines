namespace FluentPipelines;


public interface IPipe<TIn> : IPipelineComponent
{
   Task Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings);
}
public interface IPipe<TIn, TOut> : IPipe<TIn>, IPipeOut<TOut>, IAsPipeline<Pipeline_Open<TIn, TOut>>
{
   
}
