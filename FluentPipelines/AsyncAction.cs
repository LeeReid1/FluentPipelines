using System.Runtime.CompilerServices;

namespace FluentPipelines;

/// <summary>
/// Wraps an action to ensure it returns asyncronously
/// </summary>
public class AsyncAction<TIn>
{
   readonly Func<TIn, Task> func;
   public string Name { get; }
   public AsyncAction(Func<TIn, Task> f, string? nameOverride=null)
   {
      this.func = f;
      Name = nameOverride ?? f.Method.Name;
   }
   public AsyncAction(Action<TIn> f, string? nameOverride = null)
   {
      this.func = input =>
      {
         f(input);
         return Task.CompletedTask;
      };
      Name = nameOverride ?? f.Method.Name;
   }

   public Task Invoke(TIn input) => func.Invoke(input);

   public IPipe<TIn> ToPipe()
   {
      AsyncFunc<TIn, object> aF = new(async input =>
      {
         await this.func.Invoke(input);
         return new object();
      }, Name);

      return new Pipe<TIn, object>(aF);

   }

   public static implicit operator AsyncAction<TIn>(Func<TIn, Task> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
   public static implicit operator AsyncAction<TIn>(Action<TIn> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
}
