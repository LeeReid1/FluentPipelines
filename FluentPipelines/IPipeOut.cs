namespace FluentPipelines;

public interface IPipeOut<TOut> 
{
   Task AddListener(IPipe<TOut> pipe);

}

