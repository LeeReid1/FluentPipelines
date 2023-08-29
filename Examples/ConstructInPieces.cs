using FluentPipelines;

namespace Examples;

/// <summary>
/// Demonstrates a simple pipeline that is constructed in pieces
/// </summary>
internal static class ConstructInPieces
{
   public static Task RunPipeline()
   {
      var pipeline = new StartPipe<string>(Web.RetrieveExampleWebsiteText)
                     .Then(StringManipulation.SplitIntoWords)
                        .Then(CreateCountWordsPipeline()) // Append an entire branch
                        .And(SaveToFile);

      // Notice that the call to .And() above uses the result of SplitInWords again
      // This is because we packed an entire pipeline branch into one Then() call

      // Run the pipeline
      return pipeline.Run(new SharedExecutionSettings() { LogVerbosity = Verbosity.Minimal });
   }

   /// <summary>
   /// Constructs a pipeline branch that prints how many words contain the letter E
   /// </summary>
   static IAsPipeline<Pipeline_RightSealed<string[]>> CreateCountWordsPipeline()
   {
      // To construct a branch of a pipeline without a start point to graft onto
      // create a new AsyncFunc or AsyncAction object, then call Then etc in the 
      // normal way.
      // This branch will need to be grafted onto another pipeline that has
      // a StartPipe (or similar) before it can be executed
      return new AsyncFunc<string[], int>(StringManipulation.CountWordsContainingLetterE)
                                    .Then(CountToMessage)
                                    .Then(Console.WriteLine);
   }


   /// <summary>
   /// Saves a text file to disk (mock just for example)
   /// </summary>
   static Task SaveToFile(string[] lines)
   {
      // You would save as a CSV file here but
      // to avoid putting rubbish on your HDD
      // we just pretend to do that

      //return System.IO.File.WriteAllLinesAsync(@"C:\my-text-file.txt", lines);
      return Task.Delay(1000);
   }

   
   
   /// <summary>
   /// Converts the count into a message
   /// </summary>
   static string CountToMessage(int wordCount) => $"Found {wordCount} words containing the letter E";
}
