namespace FluentPipelines;

/// <summary>
/// Runs two pipes on the same input, and then joins them into one result
/// </summary>
public static class BranchAndJoinPipes
{

   #region OPEN

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4>>> BranchThenJoin<T1, T2, T3, T4>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                           Func<T2, T3> f1,
                                                                                                           Func<T2, T4> f2,
                                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5>>> BranchThenJoin<T1, T2, T3, T4, T5>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                   Func<T2, T3> f1,
                                                                                                                   Func<T2, T4> f2,
                                                                                                                   Func<T2, T5> f3,
                                                                                                                   bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6>>> BranchThenJoin<T1, T2, T3, T4, T5, T6>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                           Func<T2, T3> f1,
                                                                                                                           Func<T2, T4> f2,
                                                                                                                           Func<T2, T5> f3,
                                                                                                                           Func<T2, T6> f4,
                                                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                   Func<T2, T3> f1,
                                                                                                                                   Func<T2, T4> f2,
                                                                                                                                   Func<T2, T5> f3,
                                                                                                                                   Func<T2, T6> f4,
                                                                                                                                   Func<T2, T7> f5,
                                                                                                                                   bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                           Func<T2, T3> f1,
                                                                                                                                           Func<T2, T4> f2,
                                                                                                                                           Func<T2, T5> f3,
                                                                                                                                           Func<T2, T6> f4,
                                                                                                                                           Func<T2, T7> f5,
                                                                                                                                           Func<T2, T8> f6,
                                                                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            parallel);
   }
   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8, T9>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                                   Func<T2, T3> f1,
                                                                                                                                                   Func<T2, T4> f2,
                                                                                                                                                   Func<T2, T5> f3,
                                                                                                                                                   Func<T2, T6> f4,
                                                                                                                                                   Func<T2, T7> f5,
                                                                                                                                                   Func<T2, T8> f6,
                                                                                                                                                   Func<T2, T9> f7,
                                                                                                                                                   bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            new AsyncFunc<T2, T9>(f7),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8, T9, T10>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                                             Func<T2, T3> f1,
                                                                                                                                                             Func<T2, T4> f2,
                                                                                                                                                             Func<T2, T5> f3,
                                                                                                                                                             Func<T2, T6> f4,
                                                                                                                                                             Func<T2, T7> f5,
                                                                                                                                                             Func<T2, T8> f6,
                                                                                                                                                             Func<T2, T9> f7,
                                                                                                                                                             Func<T2, T10> f8,
                                                                                                                                                             bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            new AsyncFunc<T2, T9>(f7),
                            new AsyncFunc<T2, T10>(f8),
                            parallel);
   }


   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4>>> BranchThenJoin<T1, T2, T3, T4>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                           AsyncFunc<T2, T3> f1,
                                                                                                           AsyncFunc<T2, T4> f2,
                                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, parallel));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5>>> BranchThenJoin<T1, T2, T3, T4, T5>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                   AsyncFunc<T2, T3> f1,
                                                                                                                   AsyncFunc<T2, T4> f2,
                                                                                                                   AsyncFunc<T2, T5> f3,
                                                                                                                   bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, parallel));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6>>> BranchThenJoin<T1, T2, T3, T4, T5, T6>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                           AsyncFunc<T2, T3> f1,
                                                                                                                           AsyncFunc<T2, T4> f2,
                                                                                                                           AsyncFunc<T2, T5> f3,
                                                                                                                           AsyncFunc<T2, T6> f4,
                                                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, parallel));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                   AsyncFunc<T2, T3> f1,
                                                                                                                                   AsyncFunc<T2, T4> f2,
                                                                                                                                   AsyncFunc<T2, T5> f3,
                                                                                                                                   AsyncFunc<T2, T6> f4,
                                                                                                                                   AsyncFunc<T2, T7> f5,
                                                                                                                                   bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, parallel));
   }
   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                           AsyncFunc<T2, T3> f1,
                                                                                                                                           AsyncFunc<T2, T4> f2,
                                                                                                                                           AsyncFunc<T2, T5> f3,
                                                                                                                                           AsyncFunc<T2, T6> f4,
                                                                                                                                           AsyncFunc<T2, T7> f5,
                                                                                                                                           AsyncFunc<T2, T8> f6,
                                                                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, parallel));
   }

   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8, T9>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                                   AsyncFunc<T2, T3> f1,
                                                                                                                                                   AsyncFunc<T2, T4> f2,
                                                                                                                                                   AsyncFunc<T2, T5> f3,
                                                                                                                                                   AsyncFunc<T2, T6> f4,
                                                                                                                                                   AsyncFunc<T2, T7> f5,
                                                                                                                                                   AsyncFunc<T2, T8> f6,
                                                                                                                                                   AsyncFunc<T2, T9> f7,
                                                                                                                                                   bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, f7, parallel));
   }
   public static ThenResult<T2, Pipeline_Open<T1, DisposableTuple<T3, T4, T5, T6, T7, T8, T9, T10>>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                                                                             AsyncFunc<T2, T3> f1,
                                                                                                                                                             AsyncFunc<T2, T4> f2,
                                                                                                                                                             AsyncFunc<T2, T5> f3,
                                                                                                                                                             AsyncFunc<T2, T6> f4,
                                                                                                                                                             AsyncFunc<T2, T7> f5,
                                                                                                                                                             AsyncFunc<T2, T8> f6,
                                                                                                                                                             AsyncFunc<T2, T9> f7,
                                                                                                                                                             AsyncFunc<T2, T10> f8,
                                                                                                                                                             bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, f7, f8, parallel));
   }

   #endregion

   #region LEFT SEALED
   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input, new AsyncFunc<T2, T3>(f1), new AsyncFunc<T2, T4>(f2), parallel);
   }



   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>>> BranchThenJoin<T2, T3, T4, T5>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6>>> BranchThenJoin<T2, T3, T4, T5, T6>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           Func<T2, T6> f4,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7>>> BranchThenJoin<T2, T3, T4, T5, T6, T7>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           Func<T2, T6> f4,
                                                                                           Func<T2, T7> f5,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            parallel);
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           Func<T2, T6> f4,
                                                                                           Func<T2, T7> f5,
                                                                                           Func<T2, T8> f6,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            parallel);
   }


   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8, T9>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8, T9>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           Func<T2, T6> f4,
                                                                                           Func<T2, T7> f5,
                                                                                           Func<T2, T8> f6,
                                                                                           Func<T2, T9> f7,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            new AsyncFunc<T2, T9>(f7),
                            parallel);
   }


   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8, T9, T10>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3,
                                                                                           Func<T2, T6> f4,
                                                                                           Func<T2, T7> f5,
                                                                                           Func<T2, T8> f6,
                                                                                           Func<T2, T9> f7,
                                                                                           Func<T2, T10> f8,
                                                                                           bool parallel = false)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3),
                            new AsyncFunc<T2, T6>(f4),
                            new AsyncFunc<T2, T7>(f5),
                            new AsyncFunc<T2, T8>(f6),
                            new AsyncFunc<T2, T9>(f7),
                            new AsyncFunc<T2, T10>(f8),
                            parallel);
   }


   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, parallel));
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>>> BranchThenJoin<T2, T3, T4, T5>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, parallel));
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6>>> BranchThenJoin<T2, T3, T4, T5, T6>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           AsyncFunc<T2, T6> f4,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, parallel));
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7>>> BranchThenJoin<T2, T3, T4, T5, T6, T7>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           AsyncFunc<T2, T6> f4,
                                                                                           AsyncFunc<T2, T7> f5,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, parallel));
   }
   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           AsyncFunc<T2, T6> f4,
                                                                                           AsyncFunc<T2, T7> f5,
                                                                                           AsyncFunc<T2, T8> f6,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, parallel));
   }

   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8, T9>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8, T9>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           AsyncFunc<T2, T6> f4,
                                                                                           AsyncFunc<T2, T7> f5,
                                                                                           AsyncFunc<T2, T8> f6,
                                                                                           AsyncFunc<T2, T9> f7,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, f7, parallel));
   }
   public static ThenResult<T2, Pipeline_LeftSealed<DisposableTuple<T3, T4, T5, T6, T7, T8, T9, T10>>> BranchThenJoin<T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3,
                                                                                           AsyncFunc<T2, T6> f4,
                                                                                           AsyncFunc<T2, T7> f5,
                                                                                           AsyncFunc<T2, T8> f6,
                                                                                           AsyncFunc<T2, T9> f7,
                                                                                           AsyncFunc<T2, T10> f8,
                                                                                           bool parallel = false)
   {
      return input.Then(input => BranchThenJoin(input, f1, f2, f3, f4, f5, f6, f7, f8, parallel));
   }

   #endregion

   static async Task<DisposableTuple<T2, T3>> BranchThenJoin<T1, T2, T3>(T1 input, AsyncFunc<T1, T2> f1, AsyncFunc<T1, T3> f2, bool parallel)
   {

      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, p, p, p, p, p, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2
      };

   }

   static async Task<DisposableTuple<T2, T3, T4>> BranchThenJoin<T1, T2, T3, T4>(T1 input, AsyncFunc<T1, T2> f1, AsyncFunc<T1, T3> f2, AsyncFunc<T1, T4> f3, bool parallel)
   {


      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, f3, p, p, p, p, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2,
         Val3 = result.Val3
      };

   }
   static async Task<DisposableTuple<T2, T3, T4, T5>> BranchThenJoin<T1, T2, T3, T4, T5>(T1 input,
                                                                                         AsyncFunc<T1, T2> f1,
                                                                                         AsyncFunc<T1, T3> f2,
                                                                                         AsyncFunc<T1, T4> f3,
                                                                                         AsyncFunc<T1, T5> f4,
                                                                                         bool parallel)
   {

      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, f3, f4, p, p, p, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2,
         Val3 = result.Val3,
         Val4 = result.Val4
      };

   }
   static async Task<DisposableTuple<T2, T3, T4, T5, T6>> BranchThenJoin<T1, T2, T3, T4, T5, T6>(T1 input,
                                                                                                 AsyncFunc<T1, T2> f1,
                                                                                                 AsyncFunc<T1, T3> f2,
                                                                                                 AsyncFunc<T1, T4> f3,
                                                                                                 AsyncFunc<T1, T5> f4,
                                                                                                 AsyncFunc<T1, T6> f5,
                                                                                                 bool parallel)
   {

      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, f3, f4, f5, p, p, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2,
         Val3 = result.Val3,
         Val4 = result.Val4,
         Val5 = result.Val5
      };

   }

   static async Task<DisposableTuple<T2, T3, T4, T5, T6, T7>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7>(T1 input,
                                                                                                         AsyncFunc<T1, T2> f1,
                                                                                                         AsyncFunc<T1, T3> f2,
                                                                                                         AsyncFunc<T1, T4> f3,
                                                                                                         AsyncFunc<T1, T5> f4,
                                                                                                         AsyncFunc<T1, T6> f5,
                                                                                                         AsyncFunc<T1, T7> f6,
                                                                                                         bool parallel)
   {

      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, f3, f4, f5, f6, p, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2,
         Val3 = result.Val3,
         Val4 = result.Val4,
         Val5 = result.Val5,
         Val6 = result.Val6,
      };

   }
   static async Task<DisposableTuple<T2, T3, T4, T5, T6, T7, T8>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8>(T1 input,
                                                                                                                 AsyncFunc<T1, T2> f1,
                                                                                                                 AsyncFunc<T1, T3> f2,
                                                                                                                 AsyncFunc<T1, T4> f3,
                                                                                                                 AsyncFunc<T1, T5> f4,
                                                                                                                 AsyncFunc<T1, T6> f5,
                                                                                                                 AsyncFunc<T1, T7> f6,
                                                                                                                 AsyncFunc<T1, T8> f7,
                                                                                                                 bool parallel)
   {
      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder

      var result = await BranchThenJoin(input, f1, f2, f3, f4, f5, f6, f7, p, parallel);

      return new()
      {
         Val1 = result.Val1,
         Val2 = result.Val2,
         Val3 = result.Val3,
         Val4 = result.Val4,
         Val5 = result.Val5,
         Val6 = result.Val6,
         Val7 = result.Val7,
      };

   }

   static async Task<DisposableTuple<T2, T3, T4, T5, T6, T7, T8, T9>> BranchThenJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 input,
                                                                                                                         AsyncFunc<T1, T2> f1,
                                                                                                                         AsyncFunc<T1, T3> f2,
                                                                                                                         AsyncFunc<T1, T4> f3,
                                                                                                                         AsyncFunc<T1, T5> f4,
                                                                                                                         AsyncFunc<T1, T6> f5,
                                                                                                                         AsyncFunc<T1, T7> f6,
                                                                                                                         AsyncFunc<T1, T8> f7,
                                                                                                                         AsyncFunc<T1, T9> f8,
                                                                                                                         bool parallel)
   {

      T2 r1;
      T3 r2;
      T4 r3;
      T5 r4;
      T6 r5;
      T7 r6;
      T8 r7;
      T9 r8;
      if (parallel)
      {
         var t1 = f1.Invoke(input);
         var t2 = f2.Invoke(input);
         var t3 = f3.Invoke(input);
         var t4 = f4.Invoke(input);
         var t5 = f5.Invoke(input);
         var t6 = f6.Invoke(input);
         var t7 = f7.Invoke(input);
         var t8 = f8.Invoke(input);
         await Task.WhenAll(t1, t2, t3, t4, t5, t6, t7, t8);
         r1 = t1.Result;
         r2 = t2.Result;
         r3 = t3.Result;
         r4 = t4.Result;
         r5 = t5.Result;
         r6 = t6.Result;
         r7 = t7.Result;
         r8 = t8.Result;
      }
      else
      {
         r1 = await f1.Invoke(input);
         r2 = await f2.Invoke(input);
         r3 = await f3.Invoke(input);
         r4 = await f4.Invoke(input);
         r5 = await f5.Invoke(input);
         r6 = await f6.Invoke(input);
         r7 = await f7.Invoke(input);
         r8 = await f8.Invoke(input);
      }

      return new()
      {
         Val1 = r1,
         Val2 = r2,
         Val3 = r3,
         Val4 = r4,
         Val5 = r5,
         Val6 = r6,
         Val7 = r7,
         Val8 = r8,
      };
   }
   

   static byte DoNothing<T>(T input) => default;
}
