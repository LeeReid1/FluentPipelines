namespace FluentPipelines;

/// <summary>
/// Runs two pipes on the same input, and then joins them into one result
/// </summary>
public static class BranchAndJoinPipes
{
   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4>>> BranchThenJoin<T1, T2, T3, T4>(this Pipeline_Open<T1, T2> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2)
   {
      return BranchThenJoin(input, new AsyncFunc<T2, T3>(f1), new AsyncFunc<T2, T4>(f2));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4>>> BranchThenJoin<T1, T2, T3, T4>(this Pipeline_Open<T1, T2> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2));
   }
   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5>>> BranchThenJoin<T1, T2, T3, T4, T5>(this Pipeline_Open<T1, T2> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3)
   {
      return BranchThenJoin(input, new AsyncFunc<T2, T3>(f1), new AsyncFunc<T2, T4>(f2), new AsyncFunc<T2, T5>(f3));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5>>> BranchThenJoin<T1, T2, T3, T4, T5>(this Pipeline_Open<T1, T2> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3));
   }
   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this Pipeline_LeftSealed<T2> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2)
   {
      return BranchThenJoin(input, new AsyncFunc<T2, T3>(f1), new AsyncFunc<T2, T4>(f2));
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this Pipeline_LeftSealed<T2> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2));
   }


   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>>> BranchThenJoin<T2, T3, T4, T5>(this Pipeline_LeftSealed<T2> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3)
   {
      return BranchThenJoin(input, f1, f2, f3);
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>>> BranchThenJoin<T2, T3, T4, T5>(this Pipeline_LeftSealed<T2> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3));
   }

   static async Task<DisposableTuple<T2, T3>> BranchThenJoin<T1, T2, T3>(T1 input, AsyncFunc<T1, T2> f1, AsyncFunc<T1, T3> f2)
   {
      T2 r1 = await f1.Invoke(input);
      T3 r2 = await f2.Invoke(input);

      return new()
      {
         Val1 = r1,
         Val2 = r2
      };
   }

   static async Task<DisposableTuple<T2, T3, T4>> BranchThenJoin<T1, T2, T3, T4>(T1 input, AsyncFunc<T1, T2> f1, AsyncFunc<T1, T3> f2, AsyncFunc<T1, T4> f3)
   {
      T2 r1 = await f1.Invoke(input);
      T3 r2 = await f2.Invoke(input);
      T4 r3 = await f3.Invoke(input);

      return new()
      {
         Val1 = r1,
         Val2 = r2,
         Val3 = r3
      };
   }
}
