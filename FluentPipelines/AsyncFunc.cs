namespace FluentPipelines;

/// <summary>
/// Wraps a function to ensure it returns asyncronously
/// </summary>
public class AsyncFunc<TIn,TOut>
{
   readonly Func<TIn, Task<TOut>> func;
   public string Name { get; }

   public AsyncFunc(Func<TIn, Task<TOut>> func, string? nameOverride=null)
   {
      this.func = func;
      Name = nameOverride ?? func.Method.Name;
   }
   public AsyncFunc(Func<TIn, TOut> func, string? nameOverride = null)
   {
      this.func = input => Task.FromResult(func(input));
      Name = nameOverride ?? func.Method.Name;
   }

   [Obsolete("Input must return a value")]
   public AsyncFunc(Func<TIn, Task> func)
   {
      throw new NotSupportedException("Input must return a value");
   }

   public Task<TOut> Invoke(TIn input) => func.Invoke(input);

   public static implicit operator AsyncFunc<TIn, TOut>(Func<TIn, Task<TOut>> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
   public static implicit operator AsyncFunc<TIn, TOut>(Func<TIn, TOut> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
}
