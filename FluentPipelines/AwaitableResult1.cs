namespace FluentPipelines;

internal class AwaitableResult
{
   readonly AwaitableResult<byte> inner = new();
   public bool Completed => inner.Completed;
   
   public async Task Await()
   {
      try
      {
         await inner.Await().ConfigureAwait(false);
      }
      catch (TaskCanceledException)
      {
      }
   }

   public void SetComplete()
   {
      inner.SetResult(default);
   }
}
