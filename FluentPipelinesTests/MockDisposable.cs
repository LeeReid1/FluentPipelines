using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPipelinesTests;
internal class MockDisposable : IDisposable
{
   public bool IsDisposed => disposedValue;
   /// <summary>
   /// For debugging
   /// </summary>
   public string? Tag { get; set; }

   private bool disposedValue;
   protected virtual void Dispose(bool disposing)
   {
      if (!disposedValue)
      {
         if (disposing)
         {
            // TODO: dispose managed state (managed objects)
         }

         // TODO: free unmanaged resources (unmanaged objects) and override finalizer
         // TODO: set large fields to null
         disposedValue = true;
      }
   }

   // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
   ~MockDisposable()
   {
      //// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      //Dispose(disposing: false);
      Assert.IsTrue(IsDisposed, "Memory leak");
   }

   public void Dispose()
   {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
   }
}
