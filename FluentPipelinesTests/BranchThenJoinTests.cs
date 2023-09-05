using FluentPipelines;

namespace FluentPipelinesTests;



[TestClass]
public class BranchThenJoinTests : JoinTestsBase
{
   /// <summary>
   /// Joins two branches
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task Join_2()
   {
      // Set up
      PushStartPipe<int> start = new();
      await Join_2_Sub(start, start.BranchThenJoin(Add3, MultiplyBy5).Then(Sum));
   }
   
   /// <summary>
   /// Joins three branches
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task Join_3()
   {
      // Set up
      PushStartPipe<int> start = new();
      await Join_3_Sub(start, start.BranchThenJoin(Add3, MultiplyBy5, MultiplyBy7).Then(Sum3));
   }


   [TestMethod]
   public async Task Join_Dispose()
   {
      static ThenResult<int, Pipeline_FullySealed> JoinAndMerge(PushStartPipe<int> start, Func<int, MockDisposable> Step1a, Func<int, MockDisposable> Step1b, Func<MockDisposable, MockDisposable, int> Step2, Action<int> Step3)
      {
         return start.BranchThenJoin(Step1a, Step1b).Then(Step2).Then(Step3);
      }

      await Join_Dispose_Base(JoinAndMerge);
   }

}
