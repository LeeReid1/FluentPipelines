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
public readonly struct DisposableTuple<T1,T2, T3, T4> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }
   public T4 Val4 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
   }
}
public readonly struct DisposableTuple<T1, T2, T3, T4, T5> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }
   public T4 Val4 { get; init; }
   public T5 Val5 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
      (Val5 as IDisposable)?.Dispose();
   }
}
public readonly struct DisposableTuple<T1, T2, T3, T4, T5, T6> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }
   public T4 Val4 { get; init; }
   public T5 Val5 { get; init; }
   public T6 Val6 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
      (Val5 as IDisposable)?.Dispose();
      (Val6 as IDisposable)?.Dispose();
   }
}
public readonly struct DisposableTuple<T1, T2, T3, T4, T5, T6, T7> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }
   public T4 Val4 { get; init; }
   public T5 Val5 { get; init; }
   public T6 Val6 { get; init; }
   public T7 Val7 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
      (Val5 as IDisposable)?.Dispose();
      (Val6 as IDisposable)?.Dispose();
      (Val7 as IDisposable)?.Dispose();
   }
}
public readonly struct DisposableTuple<T1, T2, T3, T4, T5, T6, T7, T8> : IDisposable
{
   public T1 Val1 { get; init; }
   public T2 Val2 { get; init; }
   public T3 Val3 { get; init; }
   public T4 Val4 { get; init; }
   public T5 Val5 { get; init; }
   public T6 Val6 { get; init; }
   public T7 Val7 { get; init; }
   public T8 Val8 { get; init; }

   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
      (Val5 as IDisposable)?.Dispose();
      (Val6 as IDisposable)?.Dispose();
      (Val7 as IDisposable)?.Dispose();
      (Val8 as IDisposable)?.Dispose();
   }
}
