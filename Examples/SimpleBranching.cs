using FluentPipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples;

/// <summary>
/// Demonstrates a simple pipeline that branches. All see <seealso cref="ConstructInPieces"/>
/// </summary>
internal static class SimpleBranching
{
   public static Task RunSimpleBranchingPipeline()
   {

      
      var pipeline = new StartPipe<string>(Web.RetrieveExampleWebsiteText)
                     .Then(StringManipulation.SplitIntoWords)
                        .Then(SaveToFile)
                        .And(CountWordsContainingLetterE) // And takes the INPUT of the previous Then, rather than the result of the previous Then
                           .Then(CountToMessage)
                              .Then(Console.WriteLine);


      // Run the async pipeline
      return pipeline.Run(new SharedExecutionSettings() { LogVerbosity = Verbosity.Minimal });
   }

   static Task SaveToFile(string[] lines)
   {
      // You would save as a CSV file here but
      // to avoid putting rubbish on your HDD
      // we just pretend to do that

      //return System.IO.File.WriteAllLinesAsync(@"C:\my-text-file.txt", lines);
      return Task.Delay(1000);
   }

   static int CountWordsContainingLetterE(string[] words) => words.Count(word => word.Contains('e', StringComparison.InvariantCultureIgnoreCase));
   static string CountToMessage(int wordCount) => $"Found {wordCount} words containing the letter E";
}
