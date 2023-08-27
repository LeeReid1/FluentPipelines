using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPipelines;
public class SharedExecutionSettings
{
   public bool PrintNameUponExecute { get; init; } = false;
   /// <summary>
   /// In DEBUG it will print a warning if a task is Run that returns a value not passed to a later pipe
   /// </summary>
   public bool WarnIfResultUnused { get; init; } = false;

   public Action<string> Log { get; init; } = Console.WriteLine;
}
