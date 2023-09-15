namespace FluentPipelines;

public interface IPipe
{
   /// <summary>
   /// Executed when either the method this contains, or a downstream pipe throws an uncaught exception
   /// </summary>
   /// <param name="pipe"></param>
   /// <returns></returns>
   Task AddOnErrorListener(INoInputStartPipe pipe);

}
public interface IPipeOut<TOut> : IPipe
{
   Task AddListener(IPipe<TOut> pipe);

}

