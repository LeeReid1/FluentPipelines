using System.Collections;

namespace FluentPipelines;

public abstract record EmptyDisposableTuple() : IEnumerable, IDisposable
{
   private bool disposedValue;

   public abstract IEnumerator GetEnumerator();

   protected virtual void Dispose(bool disposing)
   {
      if (!disposedValue)
      {
         if (disposing)
         {
            // dispose managed state (managed objects)
            foreach (var item in this)
            {
               (item as IDisposable)?.Dispose();
            }
         }

         // free unmanaged resources (unmanaged objects) and override finalizer
         // set large fields to null
         disposedValue = true;
      }
   }

   // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
   // ~DisposableTuple()
   // {
   //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
   //     Dispose(disposing: false);
   // }

   /// <summary>
   /// Diposes any items held
   /// </summary>
   public void Dispose()
   {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
   }
}
public sealed record DisposableTuple<T1, T2>(T1 Val1, T2 Val2) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
   }
}

public sealed record DisposableTuple<T1, T2, T3>(T1 Val1, T2 Val2, T3 Val3) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
   }
}
public sealed record DisposableTuple<T1, T2, T3, T4>(T1 Val1, T2 Val2, T3 Val3, T4 Val4) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
      yield return Val4;
   }
}
public sealed record DisposableTuple<T1, T2, T3, T4, T5>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
      yield return Val4;
      yield return Val5;
   }
}
public sealed record DisposableTuple<T1, T2, T3, T4, T5, T6>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
      yield return Val4;
      yield return Val5;
      yield return Val6;
   }
}
   public sealed record DisposableTuple<T1, T2, T3, T4, T5, T6, T7>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6, T7 Val7) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
      yield return Val4;
      yield return Val5;
      yield return Val6;
      yield return Val7;
   }
}
public sealed record DisposableTuple<T1, T2, T3, T4, T5, T6, T7, T8>(T1 Val1, T2 Val2, T3 Val3, T4 Val4, T5 Val5, T6 Val6, T7 Val7, T8 Val8) : EmptyDisposableTuple
{
   public override IEnumerator GetEnumerator()
   {
      yield return Val1;
      yield return Val2;
      yield return Val3;
      yield return Val4;
      yield return Val5;
      yield return Val6;
      yield return Val7;
      yield return Val8;
   }
}
