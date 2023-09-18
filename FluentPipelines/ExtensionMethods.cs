using System.Net;

namespace FluentPipelines;
public static class ExtensionMethods
{

   public static Task Run<TPipeline>(this IAsPipeline<TPipeline> thenResult, SharedExecutionSettings? settings = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return thenResult.AsPipeline.PipelineStart.Run(settings);
   }

   #region THEN


   #region LEFT-OPEN, CLOSED


   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                            Func<T2, Task> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }


   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                               IAsPipeline<Pipeline_RightSealed<T2>> next)
   {
      var src = source.AsPipeline;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var joinTo = next.AsPipeline.PipelineStart;


      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_RightSealed<TPipelineInput>(start));
   }
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T2>(this IPipeline_Open<TPipelineInput, T2> source,
                                                                                               IAsPipeline<Pipeline_RightSealed<T2>> next)
   {
      var src = source;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var joinTo = next.AsPipeline.PipelineStart;


      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_RightSealed<TPipelineInput>(start));
   }

   #endregion

   #region LEFT-CLOSED, CLOSED

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                   Func<T2, Task> next)
   {
      return Then(source, new AsyncAction<T2>(next));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                   Action<T2> next)
   {
      return Then(source, new AsyncAction<T2>(next));
   }

   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                   Action<T2> next,
                                                                   string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
   }


   /// <summary>
   /// Connects the result of this pipeline's output to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> Then<T2>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                               IAsPipeline<Pipeline_RightSealed<T2>> next)
   {
      var src = source.AsPipeline;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var joinTo = next.AsPipeline.PipelineStart;


      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_FullySealed(start));
   }


   #endregion



   #region OPEN, OPEN



   /// <summary>
   /// Connects the result of this pipeline's output to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                    Func<T2, Task<TOut>> next,
                                                                                                    string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }

   /// <summary>
   /// Connects the result of this pipeline's output to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                    Func<T2, TOut> next,
                                                                                                    string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }

   /// <summary>
   /// Connects the result of this pipeline's output to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> Then<TPipelineInput, T2, TOut>(this IAsPipeline<Pipeline_Open<TPipelineInput, T2>> source,
                                                                                                         IAsPipeline<Pipeline_Open<T2, TOut>> next)
   {
      var src = source.AsPipeline;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = pipeline.Last;

      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_Open<TPipelineInput, TOut>(start, pipelineEnd));

   }

   #endregion

   #region LEFT-CLOSED, OPEN


   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TOut, TNext>(this Func<TOut> first, Func<TOut, TNext> next, string? name = null) => new StartPipe<TOut>(first).Then(next, name);

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                               Func<T2, Task<TOut>> next,
                                                                               string? name = null)
   {

      return Then(source, new AsyncFunc<T2, TOut>(next, name));

   }

   /// <summary>
   /// Connects the result of a Join call to the next node
   /// </summary>
   /// <remarks>Wraps methods that do not take in a disposable tuple but take in the correct arguments individually</remarks>
   public static ThenResult<DisposableTuple<TVal1, TVal2>, Pipeline_LeftSealed<TOut>> Then<TVal1, TVal2, TOut>(this IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TVal1, TVal2>>> source,
                                                                               Func<TVal1, TVal2, TOut> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<DisposableTuple<TVal1, TVal2>, TOut>(tup => next(tup.Val1, tup.Val2), name));
   }


   /// <summary>
   /// Connects the result of a Join call to the next node
   /// </summary>
   /// <remarks>Wraps methods that do not take in a disposable tuple but take in the correct arguments individually</remarks>
   public static ThenResult<DisposableTuple<TVal1, TVal2, TVal3>, Pipeline_LeftSealed<TOut>> Then<TVal1, TVal2, TVal3, TOut>(this IAsPipeline<Pipeline_LeftSealed<DisposableTuple<TVal1, TVal2, TVal3>>> source,
                                                                               Func<TVal1, TVal2, TVal3, TOut> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<DisposableTuple<TVal1, TVal2, TVal3>, TOut>(tup => next(tup.Val1, tup.Val2, tup.Val3), name));
   }


   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                               Func<T2, TOut> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }





   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                          IAsPipeline<Pipeline_Open<T2, TOut>> next)
   {
      var src = source.AsPipeline;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = pipeline.Last;

      joinFrom.AddListener(joinTo);
      return new(joinFrom, new Pipeline_LeftSealed<TOut>(start, pipelineEnd));

   }


   public static ThenResult<T2, IPipeline_RightOpen<TOut>> Then<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                               Func<T2, Task<TOut>> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }

   public static ThenResult<T2, IPipeline_RightOpen<TOut>> Then<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                               Func<T2, TOut> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<T2, TOut>(next, name));
   }
   public static ThenResult<T2, IPipeline_RightOpen<TOut>> Then<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                          IAsPipeline<Pipeline_Open<T2, TOut>> next)
   {
      var src = source.AsPipeline;
      //var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = (IPipeline_RightOpen<TOut>)new Pipeline_LeftSealed<TOut>(null!, pipeline.Last); // Null should be hidden due to interface cast

      joinFrom.AddListener(joinTo);
      return new(joinFrom, pipelineEnd);

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


   public static ThenResult<T2, Pipeline_Open<TPipelineInput, TOut>> And<TPipelineInput, T2, TOut>(this ThenResult<T2, Pipeline_Open<TPipelineInput, T2>> thenSource,
                                                                                                 IAsPipeline<Pipeline_Open<T2, TOut>> next)
   {
      var start = thenSource.Pipeline.PipelineStart;
      var pipelineEnd = next.AsPipeline.Last;

      return And_Sub(thenSource, next.AsPipeline, new Pipeline_Open<TPipelineInput, TOut>(start, pipelineEnd));

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

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> And<T2, TPipeline, TOut>(this ThenResult<T2, TPipeline> source,
                                                                                  IAsPipeline<Pipeline_Open<T2, TOut>> next)
      where TPipeline : IPipeline_LeftSealed
   {
      INoInputStartPipe start = source.Pipeline.PipelineStart;
      var pipelineEnd = next.AsPipeline.Last;

      return And_Sub(source, next.AsPipeline, new Pipeline_LeftSealed<TOut>(start, pipelineEnd));
   }


   #endregion

   #region LEFT-OPEN, CLOSED
   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, TPipeline, T2>(this ThenResult<T2, TPipeline> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
   where TPipeline : IPipeline_LeftOpen<TPipelineInput>
   {
      return And<TPipelineInput, TPipeline, T2>(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, TPipeline, T2>(this ThenResult<T2, TPipeline> source,
                                                                                                           Func<T2, Task> next,
                                                                                                            string? name = null)
   where TPipeline : IPipeline_LeftOpen<TPipelineInput>
   {
      return And<TPipelineInput, TPipeline, T2>(source, new AsyncAction<T2>(next, name));
   }

   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> And<TPipelineInput, TPipeline, T2>(this ThenResult<T2, TPipeline> source,
                                                                                                       IAsPipeline<Pipeline_RightSealed<T2>> next)
   where TPipeline : IPipeline_LeftOpen<TPipelineInput>
   {
      return And_Sub(source, next.AsPipeline, new Pipeline_RightSealed<TPipelineInput>(source.Pipeline.PipelineStart));
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
                                                                         Func<T2, Task> next,
                                                                         string? name = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return And(source, new AsyncAction<T2>(next, name));
   }

   public static ThenResult<T2, Pipeline_FullySealed> And<T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                              IAsPipeline<Pipeline_RightSealed<T2>> next)
      where TPipeline : IPipeline_LeftSealed
   {
      return And_Sub(source, next.AsPipeline, new Pipeline_FullySealed(source.Pipeline.PipelineStart));
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



   #region OnError
   #region LEFT-CLOSED, OPEN


   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> OnError<TOut, TNext>(this Func<TOut> first, Func<Task<TNext>> next, string? name = null) => new StartPipe<TOut>(first).OnError(next, name);
   public static ThenResult<TOut, Pipeline_LeftSealed<TNext>> OnError<TOut, TNext>(this Func<TOut> first, Func<TNext> next, string? name = null) => new StartPipe<TOut>(first).OnError(next, name);

   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> OnError<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                               Func<Task<TOut>> next,
                                                                               string? name = null)
   {

      return OnError(source, new StartPipe<TOut>(next, name));

   }


   /// <summary>
   /// Called when this node throws an exception 
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> OnError<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                               Func<TOut> next,
                                                                               string? name = null)
   {
      return OnError(source, new StartPipe<TOut>(next, name));
   }





   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> OnError<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                          IAsPipeline<Pipeline_LeftSealed<TOut>> next)
   {
      var src = source.AsPipeline;
      var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = pipeline.Last;

      joinFrom.AddOnErrorListener(joinTo);
      return new(joinFrom, new Pipeline_LeftSealed<TOut>(start, pipelineEnd));

   }


   public static ThenResult<T2, IPipeline_RightOpen<TOut>> OnError<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                               Func<Task<TOut>> next,
                                                                               string? name = null)
   {
      return OnError(source, new StartPipe<TOut>(next, name));
   }

   public static ThenResult<T2, IPipeline_RightOpen<TOut>> OnError<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                               Func<TOut> next,
                                                                               string? name = null)
   {
      return OnError(source, new StartPipe<TOut>(next, name));
   }
   public static ThenResult<T2, IPipeline_RightOpen<TOut>> OnError<T2, TOut>(this IAsPipeline<IPipeline_RightOpen<T2>> source,
                                                                          IAsPipeline<Pipeline_LeftSealed<TOut>> next)
   {
      var src = source.AsPipeline;
      //var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = (IPipeline_RightOpen<TOut>)new Pipeline_LeftSealed<TOut>(null!, pipeline.Last); // Null should be hidden due to interface cast

      joinFrom.AddOnErrorListener(joinTo);
      return new(joinFrom, pipelineEnd);

   }
   #endregion

   #region Open
   public static ThenResult<T2, Pipeline_Open<T1, TOut>> OnError<T1, T2, TOut>(this IAsPipeline<Pipeline_Open<T1, T2>> source,
                                                                          Func<TOut> next)
   {
      return OnError(source, new StartPipe<TOut>(next));
   }
   public static ThenResult<T2, Pipeline_Open<T1, TOut>> OnError<T1, T2, TOut>(this IAsPipeline<Pipeline_Open<T1, T2>> source,
                                                                       IAsPipeline<Pipeline_LeftSealed<TOut>> next)
   {
      var src = source.AsPipeline;
      //var start = src.PipelineStart;
      var joinFrom = src.Last;
      var pipeline = next.AsPipeline;
      var joinTo = pipeline.PipelineStart;
      var pipelineEnd = new Pipeline_Open<T1, TOut>(src.PipelineStart, pipeline.Last);

      joinFrom.AddOnErrorListener(joinTo);
      return new(joinFrom, pipelineEnd);

   }
   #endregion

   #endregion
}

