using Examples;


Console.WriteLine("Pipeline built from a value\n");
await BuildPipelineFromAValue.RunPipeline();


Console.WriteLine("\nSimple linear\n");
await SimpleLinear.Numerology();


Console.WriteLine("\nSimple Branching\n");
await SimpleBranching.RunSimpleBranchingPipeline();

Console.WriteLine("\nConstruct In Pieces\n");
await ConstructInPieces.RunPipeline();

Console.WriteLine("\nRun multiple functions on the same input, collect their return values, and feed them into the same method\n");
await BranchAndJoin.RunPipeline();

Console.WriteLine("Press any key to exit");
Console.ReadKey();