using FluentPipelines;

namespace PipelinesTests;

[TestClass]
public class UnitTest1
{
   [TestMethod]
   public async Task SimpleLinear()
   {
      int startVal = 11;
      int pipelineResult = 0;
      StartPipe<int> start = new(() => startVal);

      start.Then<int,int>(g => g * 2).Then(i=>i+7).Then(result => 
      {
         pipelineResult = result;
         return new object(); 
      });


      startVal = 11;
      await start.Run();

      Assert.AreEqual(11 * 2 + 7, pipelineResult);

      startVal = 41;
      await start.Run();
      Assert.AreEqual(41 * 2 + 7, pipelineResult);
   }

   [TestMethod]
   public async Task TwoResults()
   {
      int startVal = 11;
      int pipelineResult1 = 0;
      int pipelineResult2 = 0;
      StartPipe<int> start = new(() => startVal);

      start.Then(i => i * 2)
               .Then(i => i + 7)
                  .Then(result =>
      {
         pipelineResult1 = result;
         return new object();
      })
                  .And(result => { pipelineResult2 = result * 13; return new object(); });


      startVal = 11;
      await start.Run();

      Assert.AreEqual(11 * 2 + 7, pipelineResult1);
      Assert.AreEqual((11 * 2 + 7)*13, pipelineResult2);

   }

   [TestMethod]
   public async Task TwoResults_LongBranches()
   {
      int pipelineResult1 = 0;
      int pipelineResult2 = 0;

      var branch1 = new Pipe<int, int>(i => i * 5, "times 5").Then(i => pipelineResult1 = i, "Set result 1");

      PushStartPipe<int> start = new();

     

      start.Then(g => g * 2, "Multiply by 2")
               .Then(branch1)
               .And(MultiplyBy3, "times 3").Then(i => i - 1000, "minus 1000").Then(i=>pipelineResult2 = i, "Save Result 2");


      await start.Run(11);

      Assert.AreEqual(11 * 2 * 5, pipelineResult1);
      Assert.AreEqual(11 * 2 * 3 - 1000, pipelineResult2);

      static int MultiplyBy3(int factor) => factor * 3;

   }
   [TestMethod]
   public async Task Async()
   {
      int pipelineResult1 = 0;
      int pipelineResult2 = 0;

      var branch1 = new Pipe<int, int>(MultiplyBy5, "times 5 and wait").Then(i => pipelineResult1 = i, "Set result 1");

      StartPipe<int> start = new(() => 11);

      start.Then(g => g * 2, "Multiply by 2")
               .Then(branch1)
               .And(MultiplyBy3, "times 3 and wait")
                  .Then(i => i - 1000, "minus 1000")
                  .Then(i=>pipelineResult2 = i, "Save Result 2");


      await start.Run();

      Assert.AreEqual(11 * 2 * 5, pipelineResult1);
      Assert.AreEqual(11 * 2 * 3 - 1000, pipelineResult2);

      static async Task<int> MultiplyBy5(int factor)
      {
         await Task.Delay(300);
         return factor * 5;
      }
      static async Task<int> MultiplyBy3(int factor)
      {
         await Task.Delay(100);
         return factor * 3;
      }

   }
}