namespace FluentPipelines;


public interface IPipe<TIn>
{
   Task Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings);
}
public interface IPipe<TIn, TOut> : IPipe<TIn>, IPipeOut<TOut>, IAsPipeline<Pipeline_Open<TIn, TOut>>
{
   Pipeline_Open<TIn, TOut> IAsPipeline<Pipeline_Open<TIn, TOut>>.AsPipeline => new Pipeline_Open<TIn, TOut>(this);
}
