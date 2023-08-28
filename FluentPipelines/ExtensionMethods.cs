namespace FluentPipelines;
public static class ExtensionMethods
{

   public static Task Run<TSource, TPipeline>(this ThenResult<TSource, TPipeline> thenResult, SharedExecutionSettings? settings = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return thenResult.Pipeline.PipelineStart.Run(settings);
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
      return Then(source, new AsyncFunc<T2,TOut>(next, name));
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
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_LeftSealed<TOut>> Then<T2, TOut>(this IAsPipeline<Pipeline_LeftSealed<T2>> source,
                                                                               Func<T2, TOut> next,
                                                                               string? name = null)
   {
      return Then(source, new AsyncFunc<T2,TOut>(next, name));
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
   where TPipeline: IPipeline_LeftOpen<TPipelineInput>
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

}

