namespace FluentPipelines;

/// <summary>
/// Special pipe which uses a callback rather than expecting to hold a pipe
/// </summary>
/// <typeparam name="T"></typeparam>
class JoinIntakePipe<T> : Pipe<T, T>
{
   readonly IPipelineComponent _parent;
   readonly Func<T, SharedExecutionSettings, Task> callback;
   protected override int NoSubsequentPipes => base.NoSubsequentPipes + 1; // We overrie RunSubsequent to have to override this
   public JoinIntakePipe(IPipelineComponent parent, Func<T, SharedExecutionSettings, Task> callback) : base(a => a, string.Empty)
   {
      _parent = parent;
      this.callback = callback;
   }

   protected override IEnumerable<IPipelineComponent> GetImmediateDownstreamComponents()
   {
      return new List<IPipelineComponent>(base.GetImmediateDownstreamComponents())
         {
            _parent
         };
   }

   protected override async Task RunSubsequent(T res, SharedExecutionSettings executionSettings, bool disposeInput)
   {
      await callback.Invoke(res, executionSettings).ConfigureAwait(false);

      // The next step is for future expansion but should have nothing to do
      // NB we don't want to dispose the input because we pass it through to the next pipe unaltered
      await base.RunSubsequent(res, executionSettings, false).ConfigureAwait(false);
   }
}
