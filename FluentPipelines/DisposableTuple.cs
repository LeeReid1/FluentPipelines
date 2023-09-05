namespace FluentPipelines;

public record DisposableTuple<T1, T2>(T1 Val1, T2 Val2) : IDisposable
{
   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
   }
}

public record DisposableTuple<T1, T2, T3>(T1 Val1, T2 Val2, T3 Val3) : IDisposable
{
   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
   }
}
public record DisposableTuple<T1, T2, T3, T4>(T1 Val1, T2 Val2, T3 Val3, T4 Val4) : IDisposable
{
   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
   }
}
public record DisposableTuple<T1, T2, T3, T4, T5>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5) : IDisposable
{
   public void Dispose()
   {
      (Val1 as IDisposable)?.Dispose();
      (Val2 as IDisposable)?.Dispose();
      (Val3 as IDisposable)?.Dispose();
      (Val4 as IDisposable)?.Dispose();
      (Val5 as IDisposable)?.Dispose();
   }
}
public record DisposableTuple<T1, T2, T3, T4, T5, T6>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6) : IDisposable
{
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
public record DisposableTuple<T1, T2, T3, T4, T5, T6, T7>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6, T7 Val7) : IDisposable
{
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
public record DisposableTuple<T1, T2, T3, T4, T5, T6, T7, T8>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6, T7 Val7, T8 Val8) : IDisposable
{
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
