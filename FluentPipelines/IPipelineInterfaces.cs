using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace FluentPipelines;

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

/// <summary>
/// Holds a Pipeline
/// </summary>
public interface IPipeline_Open<TPipelineInput, TOut> : IPipeline_LeftOpen<TPipelineInput>, IPipeline_RightOpen<TOut>
{
   /// <summary>
   /// Then
   /// </summary>
   public static ThenResult<TOut, Pipeline_RightSealed<TPipelineInput>> operator  >> (IPipeline_Open<TPipelineInput, TOut> lvalue, Action<TOut> rvalue) => ExtensionMethods.Then(lvalue, new AsyncAction<TOut>(rvalue));

   /// <summary>
   /// Then
   /// </summary>
   public static ThenResult<TOut, Pipeline_RightSealed<TPipelineInput>> operator  >> (IPipeline_Open<TPipelineInput, TOut> lvalue, IAsPipeline<Pipeline_RightSealed<TOut>> rvalue) => ExtensionMethods.Then(lvalue,rvalue);   

   /// <summary>
   /// Then
   /// </summary>
   public static ThenResult<TOut, Pipeline_Open<TPipelineInput, TOut>> operator  >> (IPipeline_Open<TPipelineInput, TOut> lvalue, IAsPipeline<Pipeline_Open<TOut, TOut>> rvalue) => ExtensionMethods.Then<IPipeline_Open<TPipelineInput, TOut>,Pipeline_Open<TOut, TOut>, TPipelineInput, TOut, TOut>(lvalue, rvalue.AsPipeline);
}