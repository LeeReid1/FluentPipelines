namespace FluentPipelines;


public interface IPipe<TIn>
{
   Task Run(AutoDisposableValue<TIn> input, SharedExecutionSettings executionSettings);
}
public interface IPipe<TIn, TOut> : IPipe<TIn>, IPipeOut<TOut>
{

}
