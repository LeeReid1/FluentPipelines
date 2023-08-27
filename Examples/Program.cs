// See https://aka.ms/new-console-template for more information
using Examples;


await SimpleLinear.Numerology();


Console.WriteLine("\nSimple Branching\n");
await SimpleBranching.RunSimpleBranchingPipeline();


Console.WriteLine("\nConstruct In Pieces\n");
await ConstructInPieces.RunPipeline();

Console.WriteLine("Press any key to exit");
Console.ReadKey();