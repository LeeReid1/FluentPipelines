namespace FluentPipelines;

///// <summary>
///// A Pipeline only exposing its first, last, and second to last elements. Indended only for use within <see cref="PipeExtensionMethods"/>
///// </summary>
//public readonly record struct Pipeline<TPipelineInput, T1, T2>(IPipe<TPipelineInput> PipelineStart, IPipe<T1, T2> Last) : IExtendablePipeline<TPipelineInput, T1, T2>
//{

//}
///// <summary>
///// A Pipeline only exposing its first, last, and second to last elements that does not return a value. Indended only for use within <see cref="PipeExtensionMethods"/>
///// </summary>
//public readonly record struct SealedPipeline<TPipelineInput, T1>(IPipe<TPipelineInput> PipelineStart, IPipe<T1> Last) : ISealedPipeline<TPipelineInput, T1>
//{
//   public Pipeline<TPipelineInput, T3, T4> Then<T3,T4>(Pipeline<T2, T3, T4> concatToEnd)
//   {
//      Last.AddListener(concatToEnd.PipelineStart);
//      return new(PipelineStart, concatToEnd.SecondLast, concatToEnd.Last);
//   }
//}


public static class ExtensionMethods
{

   public static Task Run<TSource, TPipeline>(this ThenResult<TSource,TPipeline> thenResult, SharedExecutionSettings? settings = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return thenResult.Pipeline.PipelineStart.Run(settings);
   }

   #region THEN


   #region LEFT-OPEN, CLOSED


   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T1, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            Func<T2,Task> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T1, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }

   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T1, T2>(this ThenResult<T1, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                         Action<T2> next)
   {
      return Then(source.Pipeline, next);
   }
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            AsyncAction<T2> next)
   {

      return Then(source, next.ToPipe());
   }


   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                         IPipe<T2> next)
   {
      return Then(source, new Pipeline_RightSealed<T2>(next));
   }
   
   
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T1, T2>(this ThenResult<T1, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                         Pipeline_RightSealed<T2> next)
   {
      return Then(source.Pipeline, next);
   }


   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                         Pipeline_RightSealed<T2> next)
   {
      var start = source.PipelineStart;
      var joinFrom = source.Last;
      var joinTo = next.PipelineStart;


      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_RightSealed<TPipelineInput>(start));
   }

   #endregion

   #region LEFT-CLOSED, CLOSED

   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                                                      Func<T2,Task> next)
   {
      return Then(source.Pipeline, new AsyncAction<T2>(next));
   }
   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                                                      Action<T2> next)
   {
      return Then(source.Pipeline, next);
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this Pipeline_LeftSealed<T2> source,
                                                                   Action<T2> next,
                                                                   string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }

   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                   AsyncAction<T2> next)
   {
      return Then(source.Pipeline, next);
   }
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this Pipeline_LeftSealed<T2> source,
                                                               AsyncAction<T2> next)
   {

      return Then(source, next.ToPipe());
   }

   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                   IPipe<T2> next)
   {
      return Then(source.Pipeline, next);
   }
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this Pipeline_LeftSealed<T2> source,
                                                               IPipe<T2> next)
   {
      return Then(source, new Pipeline_RightSealed<T2>(next));
   }

   public static ThenResult<T2, Pipeline_FullySealed> Then<T1, T2>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                   Pipeline_RightSealed<T2> next)
   {
      return Then(source.Pipeline, next);
   }
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this Pipeline_LeftSealed<T2> source,
                                                               Pipeline_RightSealed<T2> next)
   {
      var start = source.PipelineStart;
      var joinFrom = source.Last;
      var joinTo = next.PipelineStart;


      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_FullySealed(start));
   }

   #endregion



   #region OPEN, OPEN



   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            Func<T2, Task<TOut>> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }
   
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            Func<T2, TOut> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            AsyncFunc<T2, TOut> next)
   {
      return Then(source, new Pipe<T2, TOut>(next));
   }


   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                         IPipe<T2, TOut> next)
   {
      Pipeline_Open<T2, TOut> p = new(next, next);
      return Then(source, p);
   }
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                         Pipeline_Open<T2, TOut> next)
   {
      var start = source.PipelineStart;
      var joinFrom = source.Last;
      var joinTo = next.PipelineStart;
      var pipelineEnd = next.Last;

      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_Open<TPipelineInput, TOut>(start, pipelineEnd));

   }

   #endregion

   #region LEFT-CLOSED, OPEN

   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TOut, TNext>(this Func<TOut> first, Func<TOut, TNext> next, string? name = null) => new StartPipe<TOut>(first).Then(next, name);
   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TOut, TNext>(this Func<TOut> first, AsyncFunc<TOut, TNext> next) => new StartPipe<TOut>(first).Then(next);
   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TOut, TNext>(this Func<TOut> first, Pipeline_Open<TOut, TNext> next) => new StartPipe<TOut>(first).Then(next);

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T1, T2, TOut>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                               Func<T2, Task<TOut>> next, 
                                                                               string? name=null)
   {
      return Then(source.Pipeline, new AsyncFunc<T2,TOut>(next, name));
   }
   
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T1, T2, TOut>(this ThenResult<T1, Pipeline_LeftSealed<T2>> source,
                                                                               Func<T2, TOut> next, 
                                                                               string? name=null)
   {
      return Then(source.Pipeline, next, name);
   }


   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this StartPipe<T2> source,
                                                                              Func<T2, TOut> next,
                                                                              string? name = null)
   {
      return Then(source.ToPipeline(), new AsyncFunc<T2, TOut>(next, name));
   }
   
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this Pipeline_LeftSealed<T2> source,
                                                                                                            Func<T2, TOut> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }
   
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed< TOut>> Then<T2, TOut>(this Pipeline_LeftSealed<T2> source,
                                                                                                            Func<T2, Task<TOut>> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed< TOut>> Then<T2, TOut>(this Pipeline_LeftSealed<T2> source,
                                                                                                            AsyncFunc<T2, TOut> next)
   {
      return Then(source, new Pipe<T2, TOut>(next));
   }


   public static ThenResult<T2, Pipeline_LeftSealed< TOut>> Then<T2, TOut>(this Pipeline_LeftSealed< T2> source,
                                                                                                         IPipe<T2, TOut> next)
   {
      Pipeline_Open<T2, TOut> p = new(next, next);
      return Then(source, p);
   }
   
   public static ThenResult<T2, Pipeline_LeftSealed< TOut>> Then<T1, T2, TOut>(this ThenResult<T1, Pipeline_LeftSealed< T2>> source,
                                                                                                         Pipeline_Open<T2, TOut> next)
   {
      return Then(source.Pipeline, next);
   }
   public static ThenResult<T2, Pipeline_LeftSealed< TOut>> Then<T2, TOut>(this Pipeline_LeftSealed<T2> source,
                                                                                                         Pipeline_Open<T2, TOut> next)
   {
      var start = source.PipelineStart;
      var joinFrom = source.Last;
      var joinTo = next.PipelineStart;
      var pipelineEnd = next.Last;

      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_LeftSealed< TOut>(start, pipelineEnd));

   }

   #endregion

   #endregion

   #region AND

   #region OPEN, OPEN
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> And<TPipelineInput, T2, TOut>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> thenSource,
                                                                                                   Func<T2, TOut> next,
                                                                                                   string? name = null)
   {
      return And(thenSource, new AsyncFunc<T2, TOut>(next, name));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> And<TPipelineInput, T2, TOut>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> thenSource,
                                                                                                   AsyncFunc<T2, TOut> next)
   {
      return And(thenSource, new Pipe<T2, TOut>(next));
   }


   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> And<TPipelineInput, T2, TOut>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> thenSource,
                                                                                                   IPipe<T2, TOut> next)
   {
      Pipeline_Open<T2, TOut> p = new(next, next);
      return And(thenSource, p);
   }
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> And<TPipelineInput, T2, TOut>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> thenSource,
                                                                                                   Pipeline_Open<T2, TOut> next)
   {
      var start = thenSource.Pipeline.PipelineStart;
      var pipelineEnd = next.Last;

      return And_Sub(thenSource, next, new Pipeline_Open<TPipelineInput, TOut>(start, pipelineEnd));

   }

   #endregion

   #region LEFT-CLOSED, OPEN


   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                         Func<T2, Task<TOut>> next,
                                                                                         string? name = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, new AsyncFunc<T2, TOut>(next, name));
   }
   
   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                          Func<T2, TOut> next,
                                                                                          string? name = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, new AsyncFunc<T2, TOut>(next, name));
   }


   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                    AsyncFunc<T2, TOut> next)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, (IPipe<T2, TOut>)new Pipe<T2, TOut>(next));
   }


   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                    IPipe<T2, TOut> next)
      where TPipeline : IPipeline_LeftSealed
   {
      Pipeline_Open<T2, TOut> p = new(next, next);
      return And(source, p);
   }
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                    Pipeline_Open<T2, TOut> next)
      where TPipeline : IPipeline_LeftSealed
   {
      INoInputStartPipe start = source.Pipeline.PipelineStart;
      var pipelineEnd = next.Last;

      return And_Sub(source, next, new Pipeline_LeftSealed<TOut>(start, pipelineEnd));
   }


   #endregion

   #region LEFT-OPEN, CLOSED
   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, T1, T2>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
   {
      return And(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, T2>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                            AsyncAction<T2> next)
   {

      return And(source, next.ToPipe());
   }


   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, T2>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                         IPipe<T2> next)
   {
      return And(source, new Pipeline_RightSealed<T2>(next));
   }

   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, T2>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                         Pipeline_RightSealed<T2> next)
   {
      return And_Sub(source, next, new Pipeline_RightSealed<TPipelineInput>(source.Pipeline.PipelineStart));
   }

   #endregion

   #region LEFT-CLOSED, CLOSED
   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> And<T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> And<T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                            AsyncAction<T2> next)
      where TPipeline : IPipeline_LeftSealed
   {

      return And(source, next.ToPipe());
   }


   public static ThenResult<T2, Pipeline_FullySealed> And<T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                         IPipe<T2> next)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, new Pipeline_RightSealed<T2>(next));
   }

   public static ThenResult<T2, Pipeline_FullySealed> And<T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                              Pipeline_RightSealed<T2> next)
      where TPipeline: IPipeline_LeftSealed
   {
      return And_Sub(source, next, new Pipeline_FullySealed(source.Pipeline.PipelineStart));
   }

   #endregion


   private static ThenResult<T2, TPipeline> And_Sub<T2, TPipeline, TThenPipeline>(ThenResult<T2, TThenPipeline> source,
                                                                                  IPipeline_LeftOpen<T2> next,
                                                                                  TPipeline p)
   {
      var joinFrom = source.ThenSource;
      joinFrom.AddListener(next.PipelineStart);

      return new ThenResult<T2, TPipeline>(joinFrom, p);
   }
   #endregion

}

