namespace FluentPipelines;

/// <summary>
/// Begins a pipeline using either a pre-known input, or a preknown input-calculating function
/// </summary>
/// <typeparam name="TOut">Type of input this provides to the next pipeline steps</typeparam>
public sealed class StartPipe<TOut> : StartPipeBase<TOut>, INoInputStartPipe
{
   public StartPipe(TOut inputVal, string? name = null) : this(() => inputVal, name)
   {

   }
   public StartPipe(Func<TOut> getInputVal, string? name = null) : base(new AsyncFunc<object, TOut>(o => getInputVal(), name ?? getInputVal.Method.Name))
   {

   }

   public Task Run(SharedExecutionSettings? executionSettings=null) => base.Run(new AutoDisposableValue<object>(1, new()), executionSettings ?? new());

   public override Pipeline_LeftSealed<TOut> ToPipeline() => new(this, this);
}
