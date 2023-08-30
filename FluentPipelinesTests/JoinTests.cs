using FluentPipelines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPipelinesTests;
[TestClass]
public class JoinTests
{
   [TestMethod]
   public async Task Join()
   {
      const int input = 11;
      int result = default;
      PushStartPipe<int> start = new();

      ThenResult<int, Pipeline_LeftSealed<int>> branch1 = start.Then(Add3);
      Pipeline_LeftSealed<int> branch2 = branch1.And(MultiplyBy5).Pipeline;
      var merge = branch1.Join<int, int, Pipeline_LeftSealed<int>, Pipeline_LeftSealed<int>>(branch2);

      merge.Then(Sum, name:"Sum").Then(a=> result = a);

      await start.Run(input);

      Assert.AreEqual((input + 3) + (input * 5), result);      
   }

   static int Add3(int a) => a + 3;
   static int MultiplyBy5(int a) => a * 5;

   static int Sum(DisposableTuple<int,int> tup) => tup.Val1 + tup.Val2;
}
