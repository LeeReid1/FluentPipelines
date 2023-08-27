using FluentPipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples;

/// <summary>
/// Demonstrates a simple pipeline that branches
/// </summary>
internal static class SimpleBranching
{
   public static Task RunSimpleBranchingPipeline()
   {

      
      var pipeline = new StartPipe<string>(GetContent)
                     .Then(SplitIntoWords)
                        .Then(SaveToFile)
                        .And(CountWordsContainingLetterE) // And takes the INPUT of the previous Then, rather than the result of the previous Then
                           .Then(CountToMessage)
                           .Then(Console.WriteLine);
      // Note if we call And here now, we use thee the result of CountToMessage
      // See ConstructInPieces to see how to keep using the result of SplitIntoWords

      // Run the async pipeline
      return pipeline.Run(new SharedExecutionSettings() { LogVerbosity = Verbosity.Minimal });
   }

   static string GetContent() => @"This domain is for use in illustrative examples in documents. You may use this domain in literature without prior coordination or asking for permission.";
   static string[] SplitIntoWords(string s) => s.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
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
