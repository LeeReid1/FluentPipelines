using FluentPipelines;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples;
/// <summary>
/// Demonstrates a simple linear pipeline
/// </summary>
internal static class SimpleLinear
{
   /// <summary>
   /// Demonstrates a simple linear pipeline
   /// </summary>
   public static async Task Numerology()
   {
      // This pipeline:
      // Gets the time and date
      // Determines how many seconds have passed since Jan 1st, 1990
      // Converts this into a magic number
      // Uses that to make a prediction about what will happen today
      // Prints that prediction


      // Build the pipeline
      Pipeline_FullySealed pipeline = new StartPipe<DateTime>(GetTimeInTicks)
                                                            .Then(CalculateTicksSince1990)
                                                            .Then(ConvertToMagicNumber)
                                                            .Then(PredictEvent)
                                                            .Then(prediction => "Today's Prediction is: " + prediction, "PrepareMessage")
                                                            .Then(Console.WriteLine).Pipeline;

      // Run the pipeline
      Console.WriteLine("Running with default settings:\n");
      await pipeline.PipelineStart.Run();


      // Run it again with different settings
      // We will use the Console as our Logger
      Console.WriteLine("\n******\nRunning Logging events:\n");
      SharedExecutionSettings runSettings = new()
      {
         LogVerbosity = Verbosity.Normal
      };
      await pipeline.PipelineStart.Run(runSettings);

   }

   static DateTime GetTimeInTicks() => DateTime.Today;
   static long CalculateTicksSince1990(DateTime dt) => dt.Ticks - new DateTime(1990,1,1).Ticks;
   static long ConvertToMagicNumber(long ticksDiff) => ticksDiff % 500;

   static string PredictEvent(long ticks) => ticks switch
   {
      < 100 => "Meteor Shower",
      < 200 => "Alien Abduction",
      < 300 => "Spontaneous Street Dance Party",
      < 400 => "High Profile Movie Release",
      _ => "Nothing interesting will occur today"
   };

}
