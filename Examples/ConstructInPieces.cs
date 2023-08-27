﻿using FluentPipelines;

namespace Examples;

/// <summary>
/// Demonstrates a simple pipeline that is constructed in pieces
/// </summary>
internal static class ConstructInPieces
{
   public static Task RunPipeline()
   {

      var pipeline = new StartPipe<string>(GetContent)
                     .Then(SplitIntoWords)
                        .Then(SaveToFile)
                        .And(CreateCountWordsPipeline());
      // Unlike the simple branching example, if we call .And() here now, we use the result of SplitInWords again


      // Run the async pipeline
      return pipeline.Run(new SharedExecutionSettings() { LogVerbosity = Verbosity.Minimal });
   }

   /// <summary>
   /// Constructs a pipeline branch
   /// </summary>
   static Pipeline_RightSealed<string[]> CreateCountWordsPipeline()
   {
      return new Pipe<string[], int>(CountWordsContainingLetterE)
                                    .Then(CountToMessage)
                                    .Then(Console.WriteLine).Pipeline;
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