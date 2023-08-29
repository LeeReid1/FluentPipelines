using FluentPipelines;

namespace Examples;

/// <summary>
/// Demonstrates a pipeline that branches and feeds the results of the branches back to a single method
/// </summary>
internal static class BranchAndJoin
{
   public static Task RunPipeline()
   {

      var pipeline = new StartPipe<string>(Web.RetrieveExampleWebsiteText)
                     .Then(StringManipulation.SplitIntoWords)
                        .BranchThenJoin(StringManipulation.WordsContainLetterA, 
                                        StringManipulation.WordsContainLetterE, 
                                        StringManipulation.WordsContainLetterI, 
                                        StringManipulation.WordsContainLetterO, 
                                        StringManipulation.WordsContainLetterU)
                           .Then(CountNumVowels)
                              .Then(PrintHowManyHaveOneVowel)
                              .And(PrintHowManyHave2Vowels)
                              .And(PrintHowManyHave3Vowels);

      // Run the async pipeline
      return pipeline.Run(new SharedExecutionSettings() { LogVerbosity = Verbosity.Minimal });
   }
   
   /// <summary>
   /// Demonstrates the roughly same logic as above but without using a pipeline. 
   /// </summary>
   public static async Task RunWithoutAPipeline()
   {
      Console.WriteLine($"Running {nameof(Web.RetrieveExampleWebsiteText)}");
      string text = await Web.RetrieveExampleWebsiteText();

      Console.WriteLine($"Running {nameof(StringManipulation.SplitIntoWords)}");
      string[] words = StringManipulation.SplitIntoWords(text);


      Console.WriteLine($"Running {nameof(StringManipulation.WordsContainLetterA)}");
      bool[] containA = StringManipulation.WordsContainLetterA(words);
      
      Console.WriteLine($"Running {nameof(StringManipulation.WordsContainLetterE)}");
      bool[] containE = StringManipulation.WordsContainLetterE(words);
      
      Console.WriteLine($"Running {nameof(StringManipulation.WordsContainLetterI)}");
      bool[] containI = StringManipulation.WordsContainLetterI(words);
      
      Console.WriteLine($"Running {nameof(StringManipulation.WordsContainLetterO)}");
      bool[] containO = StringManipulation.WordsContainLetterO(words);

      Console.WriteLine($"Running {nameof(StringManipulation.WordsContainLetterU)}");
      bool[] containU = StringManipulation.WordsContainLetterU(words);

      Console.WriteLine($"Running {nameof(CountNumVowels)}");
      int[] vowelCounts = CountNumVowels(containA, containE, containI, containO, containU);

      Console.WriteLine($"Running {nameof(PrintHowManyHaveOneVowel)}");
      PrintHowManyHaveOneVowel(vowelCounts);
      Console.WriteLine($"Running {nameof(PrintHowManyHave2Vowels)}");
      PrintHowManyHave2Vowels(vowelCounts);
      Console.WriteLine($"Running {nameof(PrintHowManyHave3Vowels)}");
      PrintHowManyHave3Vowels(vowelCounts);
   }


   /// <summary>
   /// Counts the number of vowels in each word
   /// </summary>
   /// <param name="vowelFound">One array per letter (A, E, I, O, U) for the existence of that letter in each word</param>
   static int[] CountNumVowels(DisposableTuple<bool[], bool[], bool[], bool[], bool[]> vowelFound) 
      => CountNumVowels(vowelFound.Val1, vowelFound.Val2, vowelFound.Val3, vowelFound.Val4, vowelFound.Val5);

   /// <summary>
   /// Counts the number of vowels in each word
   /// </summary>
   /// <returns></returns>
   static int[] CountNumVowels(params bool[][] vowelFound)
   {
      int[] counts = new int[vowelFound[0].Length];
      foreach (bool[] cur in vowelFound)
      {
         IncrementCountIfTrue(cur);
      }

      return counts;

      void IncrementCountIfTrue(bool[] arr)
      {
         for (int i = 0; i < arr.Length; i++)
         {
            if (arr[i])
               counts[i]++;
         }
      }
   }

   static void PrintHowManyHaveOneVowel(int[] vowelCounts) => MessageVowels(vowelCounts, 1);
   static void PrintHowManyHave2Vowels(int[] vowelCounts) => MessageVowels(vowelCounts, 2);
   static void PrintHowManyHave3Vowels(int[] vowelCounts) => MessageVowels(vowelCounts, 3);
   static void MessageVowels(int[] vowelCounts, int lookFor) => Console.WriteLine($"Found {vowelCounts.Count(a=>a==lookFor)} words containing {lookFor} vowels");
}
