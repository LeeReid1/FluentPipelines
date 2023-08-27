using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace FluentPipelines;


public interface INoInputStartPipe
{
   Task Run(SharedExecutionSettings? executionSettings = null);
}

/// <summary>
/// A pipeline that does not offer a way to connect the left to another pipeline's end
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
public interface IPipeline_LeftSealed
{
   INoInputStartPipe PipelineStart { get; }

}

/// <summary>
/// A pipeline that can be connected to the end of another
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="TOut"></typeparam>
public interface IPipeline_LeftOpen<TPipelineInput>
{
   IPipe<TPipelineInput> PipelineStart { get; }
}

/// <summary>
/// A pipeline that does not provide an output
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="T1"></typeparam>
public interface IPipeline_RightSealed<T1>
{
   IPipe<T1> Last { get; }
}

/// <summary>
/// A pipeline that does provide an output
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="T1"></typeparam>
public interface IPipeline_RightOpen<TOut>
{
   IPipeOut<TOut> Last { get; }

}




public interface IPipeline_FullySealed<T1> : IPipeline_LeftSealed, IPipeline_RightSealed<T1>
{

}
public interface IPipeline_FullyOpen<TPipelineInput, T1> : IPipeline_LeftOpen<TPipelineInput>, IPipeline_RightOpen<T1>
{

}

/// <summary>
/// A pipeline that does not provide an output
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="T1"></typeparam>
public interface ILeftExtendableSealedPipeline<TPipelineInput, T1> : IPipeline_LeftOpen<TPipelineInput>
{
   IPipe<T1> Last { get; }
}

/// <summary>
/// A pipeline that provides an output
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public interface IExtendablePipeline<TPipelineInput, TOut> : IPipeline_LeftSealed
{
   IPipeOut<TOut> Last { get; }
}


/// <summary>
/// A pipeline that provides an output
/// </summary>
/// <typeparam name="TPipelineInput"></typeparam>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public interface IExtendablePipelineWith2ndToLast<TPipelineInput, T1, T2> : IPipeline_LeftSealed
{
   IPipe<T1, T2> Last { get; }
}



/// <summary>
/// 
/// </summary>
/// <typeparam name="TThenSource">The pipe that was fed into Then</typeparam>
/// <typeparam name="TFullPipeline">The result of Then which can be chained with another Then or similar operation</typeparam>
/// <param name="ThenSource">The pipe that was fed into Then</param>
/// <param name="Pipeline">The result of Then which can be chained with another Then or similar operation</param>
public readonly record struct ThenResult<TThenSource, TFullPipeline>(IPipeOut<TThenSource> ThenSource, TFullPipeline Pipeline);



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


public static class PipeExtensionMethods
{
   #region THEN

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

   #region LEFT-OPEN, CLOSED
   /// <summary>
   /// Connects the result of this Then call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_RightSealed<TPipelineInput>> Then<TPipelineInput, T1, T2>(this Pipeline_Open<TPipelineInput, T2> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
   {
      return Then(source, new AsyncAction<T2>(next, name));
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
   public static ThenResult<T2, Pipeline_FullySealed> And<TPipelineInput, T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                            Action<T2> next,
                                                                                                            string? name = null)
      where TPipeline : IPipeline_LeftSealed
   {
      return And<TPipelineInput, T2, TPipeline>(source, new AsyncAction<T2>(next, name));
   }

   /// <summary>
   /// Connects the result of this And call to the next node
   /// </summary>
   public static ThenResult<T2, Pipeline_FullySealed> And<TPipelineInput, T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                            AsyncAction<T2> next)
      where TPipeline : IPipeline_LeftSealed
   {

      return And<TPipelineInput, T2, TPipeline>(source, next.ToPipe());
   }


   public static ThenResult<T2, Pipeline_FullySealed> And<TPipelineInput, T2, TPipeline>(this ThenResult<T2, TPipeline> source,
                                                                                                         IPipe<T2> next)
      where TPipeline : IPipeline_LeftSealed
   {
      return And<TPipelineInput, T2, TPipeline>(source, new Pipeline_RightSealed<T2>(next));
   }

   public static ThenResult<T2, Pipeline_FullySealed> And<TPipelineInput, T2, TPipeline>(this ThenResult<T2, TPipeline> source,
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

