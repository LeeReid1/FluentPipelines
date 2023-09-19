using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using FluentPipelines;

namespace Examples;
internal static class BuildPipelineFromAValue
{
   /// <summary>
   /// Begins a pipeline from a value
   /// </summary>
   /// <returns></returns>
   public static Task RunPipeline()
   {
      return "I'm the input".AsPipelineInput()
                            .Then(Console.WriteLine)
                            .Run();
   }
}
