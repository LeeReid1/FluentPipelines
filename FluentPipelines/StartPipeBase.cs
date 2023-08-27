namespace FluentPipelines;

public abstract class StartPipeBase<TOut> : Pipe<object, TOut>
{
   protected StartPipeBase(AsyncFunc<object, TOut> func, string? name = null) : base(func, name)
   {
   }

   /// <summary>
   /// Creates a pipeline with no input
   /// </summary>
   /// <returns></returns>
   public abstract Pipeline_LeftSealed<TOut> ToPipeline();

   public ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TNext>(Func<TOut, TNext> next, string? name = null) => ToPipeline().Then(next, name);
   public ThenResult<TOut, Pipeline_LeftSealed<TNext>> Then<TNext>(Pipeline_Open<TOut, TNext> next) => ToPipeline().Then(next);
   public ThenResult<TOut, Pipeline_FullySealed> Then<TNext>(Pipeline_RightSealed<TOut> next) => ToPipeline().Then(next);
}