using System.Diagnostics;

namespace FluentPipelines;

/// <summary>
/// Automatically disposes its value, if it is disposable, once UseComplete has been called the set number of times. Intended for internal use only
/// </summary>
/// <typeparam name="T"></typeparam>
public class AutoDisposableValue<T> : IDisposable
{
   readonly object lockObj = new();
   readonly int disposeAfter;
   /// <summary>
   /// Number of completed uses of the held value
   /// </summary>
   int useCount = 0;
   private bool isDisposed;

   T? _value;
   public T Value => _value!;
   /// <summary>
   /// True if this takes more than one call to <see cref="UseComplete"/> to be disposed
   /// </summary>
   public bool IsShared => disposeAfter > 1;

   /// <summary>
   /// Creates a new <see cref="AutoDisposableValue{T}"/>
   /// </summary>
   /// <param name="disposeAfter">This self-disposes once <see cref="UseComplete"/> has been called this many times</param>
   /// <param name="value">The value, which may be disposable</param>
   public AutoDisposableValue(int disposeAfter, T value)
   {
      this.disposeAfter = disposeAfter;
      _value = value;
   }

   /// <summary>
   /// Call to indicate that the value is no longer required
   /// </summary>
   internal void UseComplete()
   {
      lock (lockObj)
      {
         useCount++;
         Debug.Assert(useCount <= disposeAfter, "Excess call to " + nameof(UseComplete));
         if (useCount == disposeAfter)
         {
            Dispose();
         }
      }
   }

   #region Dispose Pattern
   protected virtual void Dispose(bool disposing)
   {
      if (!isDisposed)
      {
         if (disposing)
         {
            if (_value is IDisposable disposable)
            {
               disposable.Dispose();
            }
         }

         isDisposed = true;
         // Set large fields to null to encourage
         // finalisation of held resource
         _value = default;
      }
   }

   //// uncomment finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
   //~AutoDisposableValue()
   //{
   //   // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
   //   Dispose(disposing: false);
   //}

   public void Dispose()
   {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
   }
   #endregion
}
