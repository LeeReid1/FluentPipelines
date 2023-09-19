namespace FluentPipelines;

public static class AsPipelineStartExtension

{
   /// <summary>
   /// Begins building a pipeline with this as input
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="input"></param>
   /// <returns></returns>
   public static StartPipe<T> AsPipelineInput<T>(this T input) => new(input);
}