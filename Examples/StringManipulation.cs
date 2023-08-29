using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples;
internal class StringManipulation
{
   internal static string[] SplitIntoWords(string s) => s.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetterA(string[] words) => WordsContainLetter(words, 'a');
   
   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetterE(string[] words) => WordsContainLetter(words, 'e');
   
   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetterI(string[] words) => WordsContainLetter(words, 'i');
   
   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetterO(string[] words) => WordsContainLetter(words, 'o');
   
   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetterU(string[] words) => WordsContainLetter(words, 'u');
   
   /// <summary>
   /// Words containing this letter return true, else false
   /// </summary>
   internal static bool[] WordsContainLetter(string[] words, char lookFor) => words.Select(word => word.Contains(lookFor, StringComparison.InvariantCultureIgnoreCase)).ToArray();

   /// <summary>
   /// Returns how many words contain the letter E
   /// </summary>
   internal static int CountWordsContainingLetterE(string[] words) => words.Count(word => word.Contains('e', StringComparison.InvariantCultureIgnoreCase));
}
