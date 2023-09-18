using FluentPipelines;

namespace FluentPipelinesTests;

public abstract class JoinTestsBase
{
   /// <summary>
   /// Tests a join between two inputs, where A-->B-->C and A-->C
   /// </summary>
   /// <param name="start"></param>
   /// <param name="pipeline">Should Add 3 to input and join to input</param>
   /// <returns></returns>
   protected static async Task SkipConnection(PushStartPipe<int> start, ThenResult<DisposableTuple<int, int>, Pipeline_LeftSealed<int>> pipeline)
   {
      int result = default;

      // Set up
      pipeline.Then(a => result = a);

      // Run
      int input = 11;
      await start.Run(input);

      Assert.AreEqual(input + 3 + input, result);

      // Run again to make sure it's returning new values
      result = 0;
      input = 131;
      await start.Run(input);

      Assert.AreEqual((input + 3) + input, result);
   }
   
   /// <summary>
   /// Tests a join between two inputs 
   /// </summary>
   /// <param name="start"></param>
   /// <param name="pipeline"></param>
   /// <returns></returns>
   protected static async Task Join_2_Sub(PushStartPipe<int> start, ThenResult<DisposableTuple<int, int>, Pipeline_LeftSealed<int>> pipeline)
   {
      int result = default;

      // Set up
      pipeline.Then(a => result = a);

      // Run
      int input = 11;
      await start.Run(input);

      Assert.AreEqual((input + 3) + (input * 5), result);

      // Run again to make sure it's returning new values
      result = 0;
      input = 131;
      await start.Run(input);

      Assert.AreEqual((input + 3) + (input * 5), result);
   }
   
   
   /// <summary>
   /// Tests a join between two inputs 
   /// </summary>
   /// <param name="start"></param>
   /// <param name="pipeline"></param>
   /// <returns></returns>
   protected static async Task Join_3_Sub(PushStartPipe<int> start, ThenResult<DisposableTuple<int, int, int>, Pipeline_LeftSealed<int>> pipeline)
   {
      int result = default;

      // Set up
      pipeline.Then(a => result = a);

      // Run
      int input = 11;
      await start.Run(input);

      Assert.AreEqual((input + 3) + (input * 5) + (input * 7), result);

      // Run again to make sure it's returning new values
      result = 0;
      input = 131;
      await start.Run(input);

      Assert.AreEqual((input + 3) + (input * 5) + (input * 7), result);
   }

   
   internal async Task Join_Dispose_Base(Func<PushStartPipe<int>, Func<int, MockDisposable>, Func<int, MockDisposable>, Func<MockDisposable, MockDisposable, int>, Action<int>, ThenResult<int, Pipeline_FullySealed>> joinAndMerge)
   {
      MockDisposable? md1 = null;
      MockDisposable? md2 = null;
      bool step3Executed = false;

      // Set up
      PushStartPipe<int> start = new();
      joinAndMerge(start, Step1a, Step1b, Step2, Step3);
      
      // Run
      await RunAndCheck(start);

      // Run again to make sure it's returning new values
      await RunAndCheck(start);

      MockDisposable Step1a(int input) => md1 = new() { Tag = "md1" };
      MockDisposable Step1b(int input) => md2 = new() { Tag = "md2" };

      int Step2(MockDisposable val1, MockDisposable val2)
      {
         Assert.AreSame(md1, val1);
         Assert.AreSame(md2, val2);
         Assert.IsNotNull(md1);
         Assert.IsNotNull(md2);
         Assert.IsFalse(md1.IsDisposed);
         Assert.IsFalse(md2.IsDisposed);

         return default;
      }
      void Step3(int input)
      {
         // End of Step2 should dispose items in the normal way
         Assert.IsNotNull(md1);
         Assert.IsNotNull(md2);
         Assert.IsTrue(md1.IsDisposed);
         Assert.IsTrue(md2.IsDisposed);

         step3Executed = true;
      }

      async Task RunAndCheck(PushStartPipe<int> start)
      {
         step3Executed = false;
         md1 = null;
         md2 = null;

         await start.Run(default).ConfigureAwait(false);

         Assert.IsTrue(step3Executed);
         Assert.IsNotNull(md1);
         Assert.IsNotNull(md2);
         Assert.IsTrue(md1.IsDisposed);
         Assert.IsTrue(md2.IsDisposed);
      }
   }


   protected static int Add3(int a) => a + 3;
   protected static int MultiplyBy5(int a) => a * 5;
   protected static int MultiplyBy7(int a) => a * 7;

   protected static int Sum(int val1, int val2) => val1 + val2;
   protected static int Sum3(int val1, int val2, int val3) => val1 + val2 + val3;
}
