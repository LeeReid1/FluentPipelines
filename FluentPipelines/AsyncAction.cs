using System.Runtime.CompilerServices;

namespace FluentPipelines;

/// <summary>
/// Wraps an action to ensure it returns asyncronously
/// </summary>
public class AsyncAction<TIn>
{
   readonly Func<TIn, Task> func;

   public AsyncAction(Func<TIn, Task> f)
   {
      this.func = f;
   }
   public AsyncAction(Action<TIn> f)
   {
      this.func = input =>
      {
         f(input);
         return Task.CompletedTask;
      };
   }

   public Task Invoke(TIn input) => func.Invoke(input);

   public IPipe<TIn> ToPipe(string name)
   {
      AsyncFunc<TIn, object> aF = new(async input =>
      {
         await this.func.Invoke(input);
         return new object();
      });

      return new Pipe<TIn, object>(aF, name);

   }

   public static implicit operator AsyncAction<TIn>(Func<TIn, Task> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
   public static implicit operator AsyncAction<TIn>(Action<TIn> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
}
