using FluentPipelines;
using System.Diagnostics;

namespace PipelinesTests;

[TestClass]
public class ThenTests
{

   readonly Random r = new(888);

   /// <summary>
   /// Tests Then() for a pipeline which cannot be provided a value and does not return one
   /// </summary>
   [TestMethod]
   public async Task LeftClosedRightClosed()
   {
      double? val1 = null;

      StartPipe<double> s = new(r.NextDouble);
      s.Then(SetVal1);

      await s.Run();

      Assert.IsNotNull(val1);
      Assert.AreNotEqual(0, val1);

      void SetVal1(double d) => val1 = d; // don't use a lambda because x=> val1 = x  still returns x
   }

   /// <summary>
   /// Tests Then() for a pipeline which cannot be provided a value
   /// </summary>
   [TestMethod]
   public async Task LeftClosedRightClosed_Async()
   {
      double? val1 = null;
      double? val2 = null;

      StartPipe<double> s = new(r.NextDouble);
      s.Then(x => 
      {
         val1 = x;
         return x * 2;
      }).Then(SetVal2);

      await RunAndTime(s, 250);

      Assert.IsNotNull(val1);
      Assert.IsNotNull(val2);
      Assert.AreNotEqual(0, val1);
      Assert.AreEqual(val2, val1 * 2);

      async Task SetVal2(double d)
      {
         await Task.Delay(250);
         val2 = d; // don't use a lambda because x=> val1 = x  still returns x
      }
   }

   /// <summary>
   /// Tests Then() for a pipeline segment with Left open, right closed
   /// </summary>
   [TestMethod]
   public async Task LeftClosedRightOpen()
   {
      double? val1 = null;

      StartPipe<double> s = new(r.NextDouble);
      s.Then(SetVal1);

      await s.Run();

      Assert.IsNotNull(val1);
      Assert.AreNotEqual(0, val1);

      void SetVal1(double d) => val1 = d; // don't use a lambda because x=> val1 = x  still returns x
   }

   /// <summary>
   /// Tests Then() for a pipeline segment with Left open, right closed
   /// </summary>
   [TestMethod]
   public async Task LeftClosedRightOpen_Async()
   {
      double? val1 = null;
      double? val2 = null;

      StartPipe<double> s = new(r.NextDouble);
      s.Then(SetVal1).Then(SetVal2);

      await RunAndTime(s, 250);

      Assert.IsNotNull(val1);
      Assert.IsNotNull(val2);
      Assert.AreNotEqual(0, val1);
      Assert.AreEqual(val2, val1 * 2);

      async Task<double> SetVal1(double d)
      {
         await Task.Delay(125);
         val1 = d;
         return d * 2;
      }

      async Task<double> SetVal2(double d)
      {
         await Task.Delay(125);
         val2 = d; // don't use a lambda because x=> val1 = x  still returns x
         return d;
      }
   }


   /// <summary>
   /// Tests Then() for a pipeline segment with Left+Right open.
   /// </summary>
   [TestMethod]
   public async Task LeftOpen()
   {
      double? val1 = null;

      var branch = new Pipe<double, double>(Negate).Then(SetVal1); // Negate is Left+Right Open, SetVal is Right Closed

      StartPipe<double> s = new(r.NextDouble);
      s.Then(branch);

      await s.Run();

      Assert.IsNotNull(val1);
      Assert.IsTrue(val1 < 0);

      static double Negate(double d) => -d;

      void SetVal1(double d) => val1 = d; // don't use a lambda because x=> val1 = x  still returns x
   }

   /// <summary>
   /// Tests Then() for a pipeline segment with Left+Right open.
   /// </summary>
   [TestMethod]
   public async Task LeftOpen_Async()
   {
      double? val1 = null;

      var branch = new Pipe<double, double>(Negate).Then(SetVal1); // Negate is Left+Right Open, SetVal is Right Closed

      StartPipe<double> s = new(r.NextDouble);
      s.Then(branch);


      await RunAndTime(s, 250);

      Assert.IsNotNull(val1);
      Assert.IsTrue(val1 < 0);

      async static Task<double> Negate(double d)
      {
         await Task.Delay(125);
         return -d;
      }

      async Task SetVal1(double d)
      {
         await Task.Delay(125);
         val1 = d;
      }
   }



   private static async Task RunAndTime(StartPipe<double> s, int expectedMs)
   {
      Stopwatch sw = new();
      sw.Start();
      await s.Run();
      sw.Stop();

      Assert.AreEqual(expectedMs, sw.ElapsedMilliseconds, 100, "Async not respected");
   }

   


}
