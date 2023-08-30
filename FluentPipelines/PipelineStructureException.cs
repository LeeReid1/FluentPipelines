using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPipelines;
internal class PipelineStructureException : Exception
{
   public PipelineStructureException(string message) : base(message)
   {
   }
}
