namespace FluentPipelines;

public interface INoInputStartPipe : IPipe
{
   Task Run(SharedExecutionSettings? executionSettings = null);
}
