﻿using System.IO.Pipes;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace FluentPipelines;

/// <summary>
/// Begins a pipeline using either a pre-known input, or a preknown input-calculating function
/// </summary>
/// <typeparam name="TOut">Type of input this provides to the next pipeline steps</typeparam>
public sealed class StartPipe<TOut> : INoInputStartPipe, IAsPipeline<Pipeline_LeftSealed<TOut>>, IPipelineComponent
{
   readonly Pipe<object, TOut> pipe;

   Pipeline_LeftSealed<TOut> IAsPipeline<Pipeline_LeftSealed<TOut>>.AsPipeline => ToPipeline();

   public StartPipe(TOut inputVal, string? name = null) : this(() => inputVal, name)
   {

   }
   public StartPipe(Func<Task<TOut>> getInputVal, string? name = null) : this(new AsyncFunc<object, TOut>(o => getInputVal(), name ?? getInputVal.Method.Name))
   {

   }
   
   public StartPipe(Func<TOut> getInputVal, string? name = null) : this(new AsyncFunc<object, TOut>(o => getInputVal(), name ?? getInputVal.Method.Name))
   {

   }
   public StartPipe(AsyncFunc<object, TOut> func)
   {
      pipe = new Pipe<object, TOut>(func);
   }


   public Task Run(SharedExecutionSettings? executionSettings = null)
   {
      PipelineComponentMethods.CheckForRecursion(this);
      return pipe.Run(new AutoDisposableValue<object>(1, new()), executionSettings ?? new());
   }

   /// <summary>
   /// Creates a pipeline with no input
   /// </summary>
   public Pipeline_LeftSealed<TOut> ToPipeline() => new(this, pipe);

   IEnumerable<IPipelineComponent> IPipelineComponent.GetImmediateDownstreamComponents()
   {
      yield return pipe;
   }

   public Task AddOnErrorListener(INoInputStartPipe pipe) => pipe.AddOnErrorListener(pipe);



   public static ThenResult<TOut, Pipeline_FullySealed> operator >>(StartPipe<TOut> value, AsyncAction<TOut> shiftAmount) => value.Then(shiftAmount);
}
