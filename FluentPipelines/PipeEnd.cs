namespace FluentPipelines;

/// <summary>
/// A pipe that accepts input but does not return a value intended for a subsequent pipe
/// </summary>
/// <typeparam name="TIn"></typeparam>
internal class PipeEnd<TIn> : Pipe<TIn, object>
{
   protected override bool? WarnIfResultUnusedOverride => false;
   public PipeEnd(Action<TIn> finalAct, string? name=null) : base(o => { finalAct(o); return new object(); }, name)
   {
      
   }
   
   /// <summary>
   /// Creates a PipeEnd from an asynk task
   /// </summary>
   /// <param name="finalAct"></param>
   /// <param name="name"></param>
   public PipeEnd(Func<TIn, Task> finalAct, string? name=null) : base(async o => { await finalAct(o); return new object(); }, name)
   {
      
   }
}
