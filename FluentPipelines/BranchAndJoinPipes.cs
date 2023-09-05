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
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2)
   {
      return BranchThenJoin(input, new AsyncFunc<T2, T3>(f1), new AsyncFunc<T2, T4>(f2));
   }



   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>>> BranchThenJoin<T2, T3, T4, T5>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           Func<T2, T3> f1,
                                                                                           Func<T2, T4> f2,
                                                                                           Func<T2, T5> f3)
   {
      return BranchThenJoin(input,
                            new AsyncFunc<T2, T3>(f1),
                            new AsyncFunc<T2, T4>(f2),
                            new AsyncFunc<T2, T5>(f3));
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


   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T3, T4>>> BranchThenJoin<T2, T3, T4>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2)
   {
      var t1 = input.Then(f1);
      var t2 = input.Then(f2);
      return Join(t1, t2);
   }

   public static Pipeline_LeftSealed<DisposableTuple<T3, T4, T5>> BranchThenJoin<T2, T3, T4, T5>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1,
                                                                                           AsyncFunc<T2, T4> f2,
                                                                                           AsyncFunc<T2, T5> f3)
   {

      var t1 = input.Then(f1);
      var t2 = input.Then(f2);
      var t3 = input.Then(f3);
      var j  = Join(Join(t1, t2), t3);
      return j.Then((a, b) => new DisposableTuple<T3, T4, T5>(a.Val1, a.Val2, b)).Pipeline;
      
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

      return new(result.Val1, result.Val2);
   }

   static async Task<DisposableTuple<T2, T3, T4>> BranchThenJoin<T1, T2, T3, T4>(T1 input, AsyncFunc<T1, T2> f1, AsyncFunc<T1, T3> f2, AsyncFunc<T1, T4> f3, bool parallel)
   {


      AsyncFunc<T1, byte> p = new(DoNothing);// placeholder
      var result = await BranchThenJoin(input, f1, f2, f3, p, p, p, p, p, parallel);

      return new(result.Val1,
         result.Val2,
         result.Val3);

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

      return new(result.Val1,
         result.Val2,
         result.Val3,
         result.Val4);

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

      return new(result.Val1,
         result.Val2,
         result.Val3,
         result.Val4,
         result.Val5);
      
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

      return new(result.Val1,
         result.Val2,
         result.Val3,
         result.Val4,
         result.Val5,
         result.Val6);
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

      return new(result.Val1,
         result.Val2,
         result.Val3,
         result.Val4,
         result.Val5,
         result.Val6,
         result.Val7);

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

      return new(r1, r2, r3, r4, r5, r6, r7, r8);
   }


   //public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2>>> Join<T1, T2>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
   //                                                                                     Func<T1, T2> f2)
   //{
   //   // Method not strictly required, but helps compiler resolve types so
   //   // generics don't need to be written explicitly
   //   Pipeline_Open<T1, T2> p = new AsyncFunc<T1, T2>(f2).AsPipeline;

   //   return Join<T1,T2, Pipeline_LeftSealed<T1>, Pipeline_Open<T1,T2>>(f1, p);
   //}

   //public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
   //                                                                                                         Func<T1, T2> f2)
   //{
   //   Pipeline_Open<T1, T2> p = new AsyncFunc<T1, T2>(f2).AsPipeline;

   //   return Join<T1,T2>(f1, p);
   //}

   #region Join with 2
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2)
   {
      // Method not strictly required, but helps compiler resolve types so
      // generics don't need to be written explicitly
      return new Join_LeftOpen<TPipelineIn, T1, T2>(f1.AsPipeline, f2.AsPipeline.Last);
   }
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                            IAsPipeline<Pipeline_LeftSealed<T2>> f2)
   {
      // Method not strictly required, but helps compiler resolve types so
      // generics don't need to be written explicitly
      return new Join_LeftOpen<TPipelineIn, T1, T2>(f1.AsPipeline, f2.AsPipeline.Last);
   }
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2)
   {
      // Method not strictly required, but helps compiler resolve types so
      // generics don't need to be written explicitly
      return new Join_LeftOpen<TPipelineIn, T1, T2>(f1.AsPipeline.Last, f2.AsPipeline);
   }
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2>>> Join<T1, T2>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
                                                                                             IAsPipeline<Pipeline_LeftSealed<T2>> f2)
   {
      // Method not strictly required, but helps compiler resolve types so
      // generics don't need to be written explicitly
      return Join<T1, T2, Pipeline_LeftSealed<T2>>(f1, f2);
   }
   
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2>>> Join<T1, T2, TPipeline2>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
                                                                                                   IAsPipeline<TPipeline2> f2)
      where TPipeline2 : IPipeline_RightOpen<T2>
   {
      return new Join_LeftSealed<T1, T2>(f1.AsPipeline, f2.AsPipeline.Last);
   }   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2, TPipeline2>(this IAsPipeline<Pipeline_Open<TPipelineIn,T1>> f1,
                                                                                                   IAsPipeline<TPipeline2> f2)
      where TPipeline2 : IPipeline_RightOpen<T2>
   {
      return new Join_LeftOpen<TPipelineIn, T1, T2>(f1.AsPipeline, f2.AsPipeline.Last);
   }
   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2>>> Join<TPipelineIn, T1, T2, TPipeline2>(this IAsPipeline<TPipeline2> f1,
                                                                                                   IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2)
      where TPipeline2 : IPipeline_RightOpen<T1>
   {
      return new Join_LeftOpen<TPipelineIn, T1, T2>(f1.AsPipeline.Last, f2.AsPipeline);
   }
   #endregion

   #region Join with 3


   private static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join3_Sub<TPipelineIn, T1, T2, T3>(Pipeline_Open<TPipelineIn, T1> pipelineOpen, IPipeOut<T2> out2, IPipeOut<T3> out3)
   {
      return new Join_LeftOpen_3<TPipelineIn, T1, T2, T3>(pipelineOpen, out2, out3);
   }
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join3_Sub(f1.AsPipeline, f2.AsPipeline.Last, f3.AsPipeline.Last);
   }

   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T2>> f1,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f2,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join3_Sub(f2.AsPipeline, f1.AsPipeline.Last, f3.AsPipeline.Last);
   }
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                            IAsPipeline<Pipeline_LeftSealed<T2>> f2,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join3_Sub(f1.AsPipeline, f2.AsPipeline.Last, f3.AsPipeline.Last);
   }
   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T3>> f1,
                                                                                                            IAsPipeline<Pipeline_LeftSealed<T2>> f2,
                                                                                                            IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f3)
   {
      // Exists to help compile identify generic args
      return Join3_Sub(f3.AsPipeline, f2.AsPipeline.Last, f1.AsPipeline.Last);
   }
   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                                    IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join<TPipelineIn, T1, T2, T3, Pipeline_Open<TPipelineIn, T2>, Pipeline_LeftSealed<T3>>(f1, f2, f3);
   }
   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T3>> f1,
                                                                                                                    IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f2,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T2>> f3)
   {
      // Exists to help compile identify generic args
      return Join3_Sub(f2.AsPipeline, f3.AsPipeline.Last, f1.AsPipeline.Last);
   }

   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3>(this IAsPipeline<Pipeline_Open<TPipelineIn, T1>> f1,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T2>> f2,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join<TPipelineIn, T1, T2, T3, Pipeline_LeftSealed<T2>, Pipeline_LeftSealed<T3>>(f1, f2, f3);
   }
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2, T3>>> Join<T1, T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T2>> f2,
                                                                                                                    IAsPipeline<Pipeline_LeftSealed<T3>> f3)
   {
      // Exists to help compile identify generic args
      return Join<T1, T2, T3, Pipeline_LeftSealed<T2>, Pipeline_LeftSealed<T3>>(f1, f2, f3);
   }

   
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2, T3>>> Join<T1, T2, T3, TPipeline2, TPipeline3>(this IAsPipeline<Pipeline_LeftSealed<T1>> f1,
                                                                                                   IAsPipeline<TPipeline2> f2,
                                                                                                   IAsPipeline<TPipeline3> f3)
      where TPipeline2 : IPipeline_RightOpen<T2>
      where TPipeline3 : IPipeline_RightOpen<T3>
   {
      return new Join_LeftSealed_3<T1, T2,T3>(f1.AsPipeline, f2.AsPipeline.Last, f3.AsPipeline.Last);
   }   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3, TPipeline2, TPipeline3>(this IAsPipeline<Pipeline_Open<TPipelineIn,T1>> f1,
                                                                                                   IAsPipeline<TPipeline2> f2,
                                                                                                   IAsPipeline<TPipeline3> f3)
      where TPipeline2 : IPipeline_RightOpen<T2>
      where TPipeline3 : IPipeline_RightOpen<T3>
   {
      return Join3_Sub(f1.AsPipeline, f2.AsPipeline.Last, f3.AsPipeline.Last);
   }
   
   public static IAsPipeline<Pipeline_Open<TPipelineIn, DisposableTuple<T1, T2, T3>>> Join<TPipelineIn, T1, T2, T3, TPipeline2, TPipeline3>(this IAsPipeline<TPipeline2> f1,
                                                                                                   IAsPipeline<Pipeline_Open<TPipelineIn, T2>> f2,
                                                                                                   IAsPipeline<TPipeline3> f3)
      where TPipeline2 : IPipeline_RightOpen<T1>
      where TPipeline3 : IPipeline_RightOpen<T3>
   {
      return new Join_LeftOpen_3<TPipelineIn, T1, T2, T3>(f1.AsPipeline.Last, f2.AsPipeline, f3.AsPipeline.Last);
   }
   #endregion

   static byte DoNothing<T>(T input) => default;
}
