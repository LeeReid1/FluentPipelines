using Moq;
using FluentPipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelinesTests;
/// <summary>
/// Ensures data are disposed 
/// </summary>
[TestClass]
public class DataDisposedCorrectly
{
   /// <summary>
   ///  Checks passed value is diposed after it has been used in a linear pipeline
   /// </summary>
   [TestMethod]
   public async Task InputDisposed_ToIntermediate()
   {
      int disposedCount = 0;
      int completed = 0;
      Mock<IDisposable> mock = new();
      mock.Setup(x => x.Dispose()).Callback(() => disposedCount++);

      var pipeline = new StartPipe<IDisposable>(mock.Object);
      pipeline.Then(MiddleStep).Then(seven => completed++);

      Assert.AreEqual(0, disposedCount);
      Assert.AreEqual(0, completed);

      await pipeline.Run();

      Assert.AreEqual(1, completed, "Did not run to completion");
      Assert.AreEqual(1, disposedCount);

      int MiddleStep(IDisposable d)
      {
         Assert.AreEqual(0, disposedCount, "Arrived disposed");
         return 7;
      }

   }

   /// <summary>
   ///  Checks passed value is diposed after it has been used in a linear pipeline
   /// </summary>
   [TestMethod]
   public async Task InputDisposed_ToFinal()
   {
      int disposedCount = 0;
      int completed = 0;

      Mock<IDisposable> mock = new();
      mock.Setup(x => x.Dispose()).Callback(() => disposedCount++);

      var pipeline = new PushStartPipe<int>();
      pipeline.Then(seven => mock.Object).Then(FinalStep);

      Assert.AreEqual(0, disposedCount);
      Assert.AreEqual(0, completed);

      await pipeline.Run(7, new SharedExecutionSettings());

      Assert.AreEqual(1, completed, "Did not run once to completion");
      Assert.AreEqual(1, disposedCount);

      void FinalStep(IDisposable d)
      {
         Assert.AreEqual(0, disposedCount, "Arrived disposed");
         completed++;
      }
   }


   /// <summary>
   ///  Checks passed value is diposed after it has been used in a linear pipeline, when it is created by the last step of the pipeline
   /// </summary>
   [TestMethod]
   public async Task InputDisposed_FromFinal()
   {
      int disposedCount = 0;
      int completed = 0;

      Mock<IDisposable> mock = new();
      mock.Setup(x => x.Dispose()).Callback(() => disposedCount++);

      var pipeline = new StartPipe<int>(7);
      pipeline.Then(seven => 11).Then(FinalStep);

      Assert.AreEqual(0, disposedCount);
      Assert.AreEqual(0, completed);

      await pipeline.Run();

      Assert.AreEqual(1, completed, "Did not run once to completion");
      Assert.AreEqual(1, disposedCount);


      IDisposable FinalStep(int input)
      {
         Assert.AreEqual(0, disposedCount, "Arrived disposed");
         completed++;
         return mock.Object;
      }
   }


   /// <summary>
   ///  Checks passed value is diposed after it has been used in a non-linear pipeline
   /// </summary>
   [TestMethod]
   public async Task DisposableFedIntoBranchThenJoin()
   {
      await RunNonLinear(Setup);

      static Pipeline_LeftSealed<DisposableTuple<int,int>> Setup(StartPipe<IDisposable> pipeline, Func<IDisposable, int> f1, Func<IDisposable, int> f2)
      {
         return pipeline.ToPipeline().BranchThenJoin(f1, f2).Pipeline;
      }
   }


   /// <summary>
   ///  Checks passed value is diposed after it has been used in a non-linear pipeline
   /// </summary>
   [TestMethod]
   public async Task DisposableFedIntoAnd()
   {
      await RunNonLinear(Setup);

      static Pipeline_LeftSealed<int> Setup(StartPipe<IDisposable> pipeline, Func<IDisposable, int> f1, Func<IDisposable, int> f2)
      {
         
         return pipeline.Then(f1)
                        .And(f2).Pipeline;
      }
   }

   /// <summary>
   ///  Checks passed value is diposed after it has been used in a non-linear pipeline
   /// </summary>
   
   async Task RunNonLinear<T>(Func<StartPipe<IDisposable>, Func<IDisposable, int>, Func<IDisposable, int>, Pipeline_LeftSealed<T>> setupPipelineMiddle)
   {
      int disposedCount = 0;
      int middleStepACount = 0;
      int middleStepBCount = 0;
      int completed = 0;

      Mock<IDisposable> mock = new();
      mock.Setup(x => x.Dispose()).Callback(() => disposedCount++);

      var pipeline = new StartPipe<IDisposable>(mock.Object);

      setupPipelineMiddle(pipeline, MiddleStepA, MiddleStepB).Then<T>(a => completed++);
                                          
      Assert.AreEqual(0, disposedCount);
      Assert.AreEqual(0, completed);

      await pipeline.Run();

      Assert.AreEqual(1, middleStepACount, "Did not run once to completion");
      Assert.AreEqual(1, middleStepBCount, "Did not run once to completion");
      Assert.AreEqual(1, completed, "Did not run once to completion");
      Assert.AreEqual(1, disposedCount);


      int MiddleStepA(IDisposable d)
      {
         Assert.AreEqual(0, disposedCount, "Arrived disposed");
         middleStepACount++;
         return 11;
      }
      int MiddleStepB(IDisposable d)
      {
         Assert.AreEqual(0, disposedCount, "Arrived disposed");
         middleStepBCount++;
         return 13;
      }



   }
}
