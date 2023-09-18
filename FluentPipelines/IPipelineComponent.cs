namespace FluentPipelines;

public interface IPipelineComponent
{
   bool IsJoin => false;
   IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents();
}
