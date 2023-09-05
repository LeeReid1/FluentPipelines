using FluentPipelines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentPipelinesTests;

[TestClass]
public class JoinTests : JoinTestsBase
{
   /// <summary>
   /// Joins two ands
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task Join_2()
   {
      PushStartPipe<int> start = new();
      var branch1 = start.Then(Add3);
      var branch2 = branch1.And(MultiplyBy5);
      var merge = branch1.Join(branch2);

      await Join_2_Sub(start, merge.Then(Sum));
   }
   
   
   /// <summary>
   /// Joins three ands
   /// </summary>
   /// <returns></returns>
   [TestMethod]
   public async Task Join_3()
   {
      PushStartPipe<int> start = new();
      var branch1 = start.Then(Add3);
      var branch2 = branch1.And(MultiplyBy5);
      var branch3 = branch1.And(MultiplyBy7);
      var merge = branch1.Join(branch2, branch3);

      await Join_3_Sub(start, merge.Then(Sum3));
   }
   

   [TestMethod]
   public async Task Join_Dispose()
   {
      static ThenResult<int, Pipeline_FullySealed> JoinAndMerge(PushStartPipe<int> start, Func<int, MockDisposable> Step1a, Func<int, MockDisposable> Step1b, Func<MockDisposable, MockDisposable, int> Step2, Action<int> Step3)
      {
         var branch1 = start.Then(Step1a);
         var branch2 = start.Then(Step1b);
         ThenResult<int, Pipeline_FullySealed> merge = branch1.Join(branch2).Then(Step2).Then(Step3);
         return merge;
      }

      await base.Join_Dispose_Base(JoinAndMerge);

   }

}
