namespace FluentPipelines;

public readonly struct DisposableTuple<T1,T2> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
   }
}

public readonly struct DisposableTuple<T1,T2, T3> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
   }
}
