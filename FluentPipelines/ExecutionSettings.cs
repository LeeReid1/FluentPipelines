namespace FluentPipelines;

public class SharedExecutionSettings
{
   /// <summary>
   /// In DEBUG it will print a warning if a task is Run that returns a value not passed to a later pipe
   /// </summary>
   public bool WarnIfResultUnused { get; init; } = false;

   public Action<string> Log { get; init; } = Console.WriteLine;
   public Verbosity LogVerbosity { get; init; } = Verbosity.None;
}
