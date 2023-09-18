namespace FluentPipelines;

/// <summary>
/// Wraps a function to ensure it returns asyncronously
/// </summary>
public sealed class AsyncFunc<TIn,TOut> : IAsPipeline<Pipeline_Open<TIn, TOut>>
{
   readonly Func<TIn, Task<TOut>> func;
   public string Name { get; }

   Pipeline_Open<TIn, TOut>? _asPipeline;
   public Pipeline_Open<TIn, TOut> AsPipeline
   {
      get => _asPipeline ??= new(new Pipe<TIn, TOut>(this));
   }

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

   [Obsolete("Input must return a value that is not a vanilla Task. Do you have an issue with an earlier Then or And call?")]
   public AsyncFunc(Func<TIn, Task> func)
   {
      throw new NotSupportedException("Input must return a value");
   }

   public Task<TOut> Invoke(TIn input) => func.Invoke(input);

   public static implicit operator AsyncFunc<TIn, TOut>(Func<TIn, Task<TOut>> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
   public static implicit operator AsyncFunc<TIn, TOut>(Func<TIn, TOut> func) => new(func); // DO NOT REVERSE ORDER OF THESE TWO OPERATORS OR TOUT CAN BECOME TASK
}
