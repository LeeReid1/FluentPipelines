namespace FluentPipelines;

internal class AwaitableResult<T>
{
   readonly CancellationTokenSource cancellationTokenSource = new();
   readonly Task eternalDelay;

   T? result;

   public bool Completed => eternalDelay.IsCanceled;
   
   public AwaitableResult()
   {
      eternalDelay = Task.Delay(-1, cancellationTokenSource.Token);
   }
   public async Task<T> Await()
   {
      try
      {
         await eternalDelay.ConfigureAwait(false);
      }
      catch (TaskCanceledException)
      {
      }

      return result!;
   }

   public void SetResult(T result)
   {
      if(cancellationTokenSource.IsCancellationRequested)
      {
         throw new InvalidOperationException($"{nameof(AwaitableResult<T>)} already has a value");
      }

      this.result = result;
      cancellationTokenSource.Cancel();
   }

}
