namespace FluentPipelines;

public interface IPipelineComponent
{
   bool IsJoin => false;


   IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents();
   
   
}


internal static class PipelineComponentMethods
{

   /// <summary>
   /// Throws a <see cref="PipelineStructureException"/> if recursion is found
   /// </summary>
   /// <param name="start">The pipeline start</param>
   /// <exception cref="PipelineStructureException">Upon recursion discovered</exception>
   internal static void CheckForRecursion(IPipelineComponent start)
   {
      HashSet<IPipelineComponent> addTo = new();
      Sub(start);

      void Sub(IPipelineComponent pc)
      {
         if (addTo.Add(pc))
         {
            foreach (var item in pc.GetImmediateDownstreamComponents())
            {
               Sub(item);
            }
         }
         else if (!pc.IsJoin) // Joins will appear more than once
         {
            throw new PipelineStructureException("Pipeline recurses, which is not supported");
         }
      }
   }
}