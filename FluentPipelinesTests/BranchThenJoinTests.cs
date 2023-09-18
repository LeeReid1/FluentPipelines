using FluentPipelines;

namespace FluentPipelinesTests;



[TestClass]
public class SkipConnectionTests : JoinTestsBase
{
   /// <summary>
   /// Skip connection
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task FromStart()
   {
      // A --> B ------
      // |            |
      // |            V
      // ------------>C

      // Set up
      PushStartPipe<int> start = new();
      await SkipConnection(start, start.SkipConnection(Add3).Then(Sum));
   }
   /// <summary>
   /// Skip connection
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task FromMidway()
   {
      // A --->B --> C ------
      //       |            |
      //       |            V
      //       ------------>D

      // Set up
      int result = default;
      PushStartPipe<int> start = new();
      var pipeline = start.Then(MultiplyBy5).SkipConnection(Add3).Then(Sum).Then(a=>result = a);

      await start.Run(11);
      Assert.AreEqual(11 * 5 + 3 + 11 * 5, result);
      
      await start.Run(131);
      Assert.AreEqual(131 * 5 + 3 + 131 * 5, result);
   }

}


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
