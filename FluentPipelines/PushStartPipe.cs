
namespace FluentPipelines;

/// <summary>
/// The start of a pipeline where the value is provided when <see cref="Run(TOut, Pipelines.SharedExecutionSettings?, bool)"/> is called
/// </summary>
/// <typeparam name="TOut"></typeparam>
public sealed class PushStartPipe<TOut> : IAsPipeline<Pipeline_LeftSealed<TOut>>
{
   readonly object lockObj = new();
   readonly StartPipe<TOut> pipe;

   TOut? lastValue = default;

   Pipeline_LeftSealed<TOut> IAsPipeline<Pipeline_LeftSealed<TOut>>.AsPipeline => ToPipeline();

   public PushStartPipe(string? name=null)
   {
      pipe = new(GetLastValue, name);
   }

   TOut GetLastValue() => lastValue ?? throw new NotSupportedException($"Call {nameof(Run)} from {nameof(PushStartPipe<TOut>)} rather than from {nameof(INoInputStartPipe)}");


   /// <summary>
   /// Runs the pipeline with the provided input
   /// </summary>
   /// <param name="value">Warning - this will be disposed of by the pipeline</param>
   /// <param name="executionSettings"></param>
   /// <param name="disposeInput">Whether to dispose the input upon completion</param>
   /// <returns></returns>
   public Task Run(TOut value, SharedExecutionSettings? executionSettings = null)
   {
      lock (lockObj)
      {
         try
         {
            lastValue = value;
            return pipe.Run(executionSettings);
         }
         finally
         {
            lastValue = default;
         }
      }
   }

   /// <summary>
   /// Creates a pipeline with no input
   /// </summary>
   /// <returns></returns>
   public Pipeline_LeftSealed<TOut> ToPipeline()
   {
      lock (lockObj)
      {
         return this.pipe.ToPipeline();
      }
   }

}
