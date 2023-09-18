namespace FluentPipelines;

public static class SkipConnectionExtension
{
   #region OPEN
   public static IAsPipeline<Pipeline_Open<T1, DisposableTuple<T2, T3>>> SkipConnection<T1, T2, T3>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                        Func<T2, T3> f1,
                                                                                                        bool parallel = false)
   {
      return SkipConnection(input,
                            new AsyncFunc<T2, T3>(f1),
                            parallel);
   }

   public static IAsPipeline<Pipeline_Open<T1, DisposableTuple<T2, T3>>> SkipConnection<T1, T2, T3>(this IAsPipeline<Pipeline_Open<T1, T2>> input,
                                                                                                        AsyncFunc<T2, T3> f1,
                                                                                                        bool parallel = false)
   {
      var r = input.Then(f1);
      return input.Join(r);
   }

   #endregion

   #region LEFT SEALED

   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T2, T3>>> SkipConnection<T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                        Func<T2, T3> f1)
   {
      return SkipConnection(input, new AsyncFunc<T2, T3>(f1));
   }


   /// <summary>
   /// Provides input to the function, then returns the input and the function together
   /// </summary>
   public static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T2, T3>>> SkipConnection<T2, T3>(this IAsPipeline<Pipeline_LeftSealed<T2>> input,
                                                                                           AsyncFunc<T2, T3> f1)
   {
      var t1 = input.Then(f1);
      return input.Join(t1);
   }

   #endregion

   static IAsPipeline<Pipeline_LeftSealed<DisposableTuple<T1, T2>>> SkipConnection<T1, T2>(T1 input, AsyncFunc<T1, T2> f1, bool parallel)
   {

      var t1 = new StartPipe<T1>(input);
      var t2 = t1.Then(f1);
      return t1.Join(t2);
   }

}
