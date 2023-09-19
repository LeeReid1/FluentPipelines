# Fluent Pipelines

Lets you use existing C# code - for virtually anything - in the way you currently use LINQ with little or no adaptation. This is particularly helpful for building heavy or long typesafe data processing pipelines.

Pipelines and sections of pipelines built this way:
* Are much easier to read and maintain
* Are typesafe
* Are multithreaded and threadsafe
* Are unit testable
* Eliminate your need to keep track of `async` code
* Automatically dipose `IDiposable` objects once they are not required
* Allow you apply execution settings globally without creating globally available objects
* Can be defined at multiple levels, in line with 'clean code' principles

## Example

Consider the following code, designed to ask for a webpage, download it, and count the number of words.

Normally, this may look like:

```csharp
static async Task<string> DownloadString(string site)
{
   using WebClient client = new();
   return async client.DownloadStringAsync(site).ConfigureAwait(false);
}

static async Task<string> AskUserForWebsite() => ... // TODO: Prompt the user to provide a website



async Task CountWordsInSite()
{
   string website = await AskUserForWebsite().ConfigureAwait(false);

   string websiteContent = await DownloadString(website).ConfigureAwait(false);

   string[] words = websiteContent.Split(' ');

   int numberOfWords = words.Length;

   string message = $"There were {numberOfWords} words";

   Console.WriteLine(message);
}


// Call the method
await CountWordsInSite();

```

Fluent Pipelines lets us instead write this instead as

```csharp

AskUserForWebsite.AsPipelineInput()
                 .Then(DownloadString)
                 .Then(SplitIntoWords)
                 .Then(CountWords)
                 .Then(numberOfWords => $"There were {numberOfWords} words")
                 .Then(Console.WriteLine)
                 .Run();


static string[] SplitIntoWords(string s) => s.Split(' ');

```

## Primary Operations

In pratice, most uses rely on calls to only a few methods which construct the graph and run it.

In the following example, let `A`,`B`,`C`,`D` be methods, `Func<T,S>` objects, `Func<T,Task<S>>` objects, start points, or the result of a previous call to the extension methods listed below. 

### Start Points

#### Value

Begin by calling `AsPipelineInput()` from any value to begin a pipeline.

```csharp

"input into the pipeline".AsPipelineInput()

```

This creates a StartPipe automatically (see below) which can be used for other functions such as `Then()`.

#### Function or Value

Begin with a `StartPipe`, which wraps a method or value.

Once the start point exists, we can use the extension methods stated below

```csharp
StartPipe<string> start = new(A); // First step will call A()

// Build the pipeline

// Run it
start.Run();
```

There is also `PushStartPipe` which lets you provide the input value when you call `Run()`.

```csharp
PushStartPipe<string> start = new();

// Build the pipeline

// Run it
start.Run("This sentence is the input to the pipeline");
```



### Then
`.Then()` passes the `return` value of the previous function to the next function

```csharp
A.Then(B);
```
```
A -> B
```

### And
`.And()` which passes the input of the last then call to the next function, effectively branching a pipeline

```csharp
A.Then(B)
 .And(C);
```
```
A
| -> B
| -> C
```

### Join
`.Join()` which collects `return` values from branches and feeds them all into one function

```csharp
B.Join(C).Then(D);
```
```
B -> |
     | --> D
C -> |
```

### BranchAndJoin
`.BranchAndJoin()` which performs `Then`, `And`, and `Join`

```csharp
A.BranchThenJoin(B, C).Then(D);
```
```
     | -> B -> |
A -> |         | --> D
     | -> C -> |

```

### SkipConnection
`.SkipConnection()` which performs `Then`, and `Join` to the original input.

`D`, here, receives the results of both A and B:

```csharp
A.SkipConnection(B).Then(D);
```
```
A -> | -> B -> |
     |---------|--> D

```

For example

```csharp

static string A() => ...
static int B(string input) => ...
static double C(string input) => ...
static void D(int input1, double input2) => ...

StartPipe<string> start = new(A); // First step will call A()

// Build the pipeline
start.BranchThenJoin(B,C).Then(D);


// Run it
await start.Run();
```


## Async

Methods that form part of pipelines can be async (return `Task<T>`) or otherwise (return `T`).
This does not change the syntax you use to construct them.

The only `await` call required is the call to `Run()`. 

For example, these input methods...

```csharp
static string A() => ...
static int B(string input) => ...
static double C(string input) => ...
static void D(int input1, double input2) => ...
```


...and these...

```csharp
static Task<string> A() => ...
static Task<int> B(string input) => ...
static Task<double> C(string input) => ...
static Task D(int input1, double input2) => ...
```


...could both be connected and run using:

```csharp
// Build the pipeline
StartPipe<string> start = new(A);
start.BranchThenJoin(B,C).Then(D);

await Run(start);
```

Syncronous and asyncronous code can be mixed and matched without change to the pipeline code.

## IDisposable

Stages in the pipelines automatically dispose any returned `IDisposable` the moment they are no longer required. 

For example, the following code prints how many words are on example.com

```csharp
StartPipe<WebClient> start = new(() => new WebClient());

start.Then(client=>client.DownloadString("https://example.com")
     .Then(CountWords)
     .Then(Console.WriteLine);
```

The `WebClient` is created only when the pipeline is run, and is Disposed automatically before `CountWords` is called. Running the pipeline a second time will create a new `WebClient` and also Dispose that in turn.

Any `IDisposable` object created within a method but not returned must still be disposed in the normal way.

## Branching and Parallelisation

So far we've seen purely linear pipelines. Pipelines, however, can branch by one stage outputting to more than one method.

While to pass a result to the next stage, we use `Then()`. We can do this multiple times, or more easily by using `And`.

For example, the following code counts how many words and how many numbers are in a webpage.

```csharp
StartPipe<string> start = new(AskUserForWebsite);

start.Then(DownloadString)
     .Then(SplitIntoWords)
     .Then(CountWords)
     .And(CountNumbers);
```

The `And` call above sets up `CountNumbers` to recieve the result from `SplitWords()`, rather than `CountWords()`.

Alternatively, we could use multiple Then operations. For example, if we wanted to branch to print how many words, and also to save to a database how many numbers we found, our code might look like:

```csharp
StartPipe<string> start = new(AskUserForWebsite);

var branchPoint = start.Then(DownloadString);

branchPoint.Then(CountWords)
           .Then(Console.WriteLine);

branchPoint.Then(CountNumbers)
           .Then(SaveNumberCountToDatabase());


// Run the pipeline
await start.Run();

```

### Building Branches Explicitly

Branches can be build without a start point, then connected later. 

In the code below, two static methods generate Pipeline branches that lack start points. These can be fed into an `Then` or `And` call to connect them to a pipeline start point.

```csharp
/// <summary>
/// Constructs a pipeline branch that prints how many words are in the string
/// </summary>
static IAsPipeline<Pipeline_RightSealed<string>> CreateCountWordsPipeline()
{
    return new AsyncFunc<string, string[]>(SplitIntoWords)
                                .Then(CountToMessage)
                                .Then(Console.WriteLine);
}

/// <summary>
/// Constructs a pipeline branch that calculates how many numbers are in the string and saves it to a database
/// </summary>
static IAsPipeline<Pipeline_RightSealed<string>> CreateAndSaveNumbers()
{
    return new AsyncFunc<string>(FindNumbersWithRegex)
                                .Then(CountResult)
                                .Then(SaveToDB);
}

// We can use this and similar when needed
StartPipe<string> start = new(AskUserForWebsite);



start.Then(CreateCountWordsPipeline())
     .And(CreateAndSaveNumbers());


start.Run();

```

### Parallelisation and Thread Safety

All pipeline stages that takes in the same input are run in parallel (via standard c# tasks). For this reason, all objects created by pipeline stages must be threadsafe if they will be sent to 2+ branches via `And()`, or other kinds of branching. 

For example, this is OK, because `And` is not used with the Dictionary:

```
/// <summary>
/// Calculates word counts in a string
/// </summary>
static Dictionary<string,int> GetWordCounts(string[] fullText) => ...

// We can use this and similar when needed
StartPipe<string[]> start = new(GetStrings);

start.Then(GetWordCounts)
     .Then(PrintDictionary);

```

The following is NOT OK because Dictionary is used by two branches at the same time but is not threadsafe.

```
start.Then(GetWordCounts)
     .Then(PrintDictionary)
     .And(PrintDictionaryCount);

```

In such a case a threadsafe dictionary class should be used.

#### Don't Modify Inputs With Branching

It is ONLY safe to modify an input when it is NOT sent to 2+ branches. In situations where it is sent to multiple branches, it should be treated as strictly ReadOnly.

This is because both branches will process the same input at the same time and so one will modify it as the other reads it. As there is no guarantee which branch will run when, your pipeline will not be threadsafe.

In other words, while this is safe:

```csharp

static ConcurrentDictionary<string,int> GetWordCounts(string fullText) => ...

static ConcurrentDictionary<string,int> ChangeBadgerCounts(ConcurrentDictionary<string,int> dict)
{
   dict["badger"] = 41;
   return dict;
}

static int GetBadgerCount(ConcurrentDictionary<string,int> dict) =>  dict["badger"];


// We can use this and similar when needed
StartPipe<string[]> start = new(GetStrings);

start.Then(GetWordCounts)
     .Then(ChangeBadgerCounts)
     .Then(PrintDictionary);

```

The following is not, because the last two stages modify and read the dictionary at the same time:

```csharp

// We can use this and similar when needed
StartPipe<string[]> start = new(GetStrings);

start.Then(GetWordCounts)
     .Then(ChangeBadgerCounts)
     .And(PrintDictionary);

```


#### Don't Pass IDisposable through when branched

Values passed between stages are disposed after the receiving stage has completed use of that input. When an `IDisposable` is passed to two or more branches, it is disposed once all branches have completed.

`IDisposable` inputs can be passed as outputs when NO branching is occurring. `GetBadgetCounts` does this below:

```csharp

static ConcurrentDictionary<string,int> GetWordCounts(string fullText) => ...

static ConcurrentDictionary<string,int> ChangeBadgerCounts(ConcurrentDictionary<string,int> dict)
{
   dict["badger"] = 41;
   return dict; // Return the same input
}

static int GetBadgerCount(ConcurrentDictionary<string,int> dict) =>  dict["badger"];


// We can use this and similar when needed
StartPipe<string[]> start = new(GetStrings);

start.Then(GetWordCounts)
     .Then(ChangeBadgerCounts)
     .Then(PrintDictionary);

```

But this should NOT be done if this is part of a branch, like so:

```csharp

// We can use this and similar when needed
StartPipe<string[]> start = new(GetStrings);

start.Then(GetWordCounts)
     .Then(PrintDictionary)
     .And(ChangeBadgerCounts).Then(PrintDictionary)
```

Although this may run smoothly, this makes it difficult for the system to track the IDisposable object, so it may be disposed prematurely or not at all. It also is difficult for the programmer to understand threadsafety concerns. It's best to copy the input and pass that copy down the chain instead.


## Joining Branches

Branches can be joined so their outputs can be used together. The easiest way is to use `BranchThenJoin`

Original Code:

```csharp

static async Task<Animal[]> GetAnimals() => ...
static int CountBadger(Animal[] anims) => anims.OfType<Badger>().Count();
static int CountRobbin(Robbin[] anims) => anims.OfType<Robbin>().Count();

static void PrintMessage(int noBadgers, int noRobbins) => Console.WriteLine($"Found {noBadgers} badgers and {noRobbins} robbins");

static async Task Pipeline()
{
   Animal[] animals = await GetAnimals().ConfigureAwait(false);
   int badgerCount = CountBadger(animals);
   int robbinCount = CountRobbin(animals);
   PrintMessage(badgerCount, robbinCount);
}

await Pipeline();
```

As a Fluent Pipeline

```csharp

// We can use this and similar when needed
StartPipe<Animal[]> start = new(GetAnimals);

start.BranchThenJoin(CountBadger, CountRobbin)
     .Then(PrintMessage)


await start.Run();
```


## Clean Code and Treating Branches as Objects

Code is cleanest and most readable when operations are abstracted to occur at the same 'level'. For example, the following:

```csharp

StartPipe<Animal[]> getAnimals = new(GetAnimals);

getAnimals.Then(PrintBadgerCount)

```

is much more readable than:

```csharp

StartPipe<Animal[]> getAnimals = new(GetAnimals);

getAnimals.Then(IdentifyHairyAnimals)
          .BranchThenJoin(IdentifySnouts, IdentifyPaws, IdentifyBlackAndWhite)
          .Then(LimitToHairyAnimalsWithSnoutsAndFourPawsAndCorrectColour)
          .Then(a=>a.Count)
          .Then(PrintBadgerCount);
```

It's also better designed, because the the first has two high-level operations - a data fetch and user message - while the second contains high level operations and other steps that only form part of the process of finding badgers.

This becomes clear when want to also count robbins:

```csharp

StartPipe<Animal[]> getAnimals = new(GetAnimals);

getAnimals.Then(PrintBadgerCount)
          .And(PrintRobbinCount);

```

Which is possible but much more difficult in the second example. In this way, Fluent Pipelines start to enforce clean code principles.

You can create sections of pipeline in FluentPipelines then connect them later to provide easier to read code. In the example above, we'd create a method called `GetCountBadgers()` which would create a pipeline that accepted `Animal[]` and output an `int`.

Usually, the return types of these methods are very complex due to type safety constraints, and generally not intended for human eyes. It's best to write your method using `var`, then ask Visual Studio to convert that into an explicit type only as needed (such as to have a method return type).

A result might look something like

```csharp

static class BadgerIdentifier
{

   /// <summary>
   /// Creates a section of pipeline that prints how many badgers there are
   /// </summary>
   ThenResult<Animal[], Pipeline_RightSealed<Animal[], int>> GetPrintCount()
   {
      return GetCount().Then(Console.WriteLine);
   }

   /// <summary>
   /// Creates a section of pipeline that counts how many badgers
   /// </summary>
   ThenResult<Animal[], Pipeline_Open<Animal[], int>> GetCount()
   {
      return new AsyncMethod<Animal>(IdentifyHairyAnimals)
                 .BranchThenJoin(IdentifySnouts, IdentifyPaws, IdentifyBlackAndWhite)
                 .Then(LimitToHairyAnimalsWithSnoutsAndFourPawsAndCorrectColour)
                 .Then(a=>a.Count);
   }
}

static class RobbinIdentifier
{
   ...
} 


```


We could use these like:

```csharp


StartPipe<Animal[]> getAnimals = new(GetAnimals);

getAnimals.Then(BadgerIdentifier.GetPrintCount())
          .And(RobbinIdentifier.GetPrintCount());


```


## Some Recipes


### A Skipped Connection


```
A --> B-----> C
|             ↑ 
--------------|
```

Might be written:

```csharp

var StepA = new StartPipe(A).SkipConnection(B).Then(C);

```

Which is shorthand for

```csharp

var stepA = new StartPipe(A);
var stepB = StepA.Then(B);
var pipeline = stepA.Join(stepB).Then(C);

```


## Best practices with more complex code

Fluent pipelines are best for code that branches minimally. When complex branching and merging occurs you may need to split pipeline building into several steps to maintain legibility.

For example, lets say we have code that looks like the following (where letters are methods):

```

A --> B------------
|         |       |
|         V       V
|         C ----> F
|                 |
|                 V
|-------> D ----->E
```

Might be written:

```csharp

var stepA = new StartPipe(A);

var bToF = stepA.Then(B).SkipConnection(C).Then(F);

var stepD = stepA.Then(D).Join(bToF).Then(E);

```

Whlie this might seem let fluent than other examples, a pipeline like this is more likely to represent three stages:
* A: Data I/O
* B, C, F: Process the image
* D: Ask where to save the image to
* E: Save the image

Best practice would always be to keep these branches apart, as we have done, which means more readable code:

```csharp

// Open the image
var imageInput = new StartPipe(OpenImage);

// Mask the face out
var maskTheFace = ImageInput.Then(IdentifyFace)
                            .SkipConnection(CreateMask)
                            .Then(MaskFace);

var fullPipeline = imageInput.Then(AskUserForSaveLocation)
                             .Join(maskTheFace)
                             .Then(SaveImage);


await fullPipeline.Run();
```

### Reusing an object

It's cleanest to make separate branches, rather than `And` when using a single object for many tasks. Always make sure the object is threadsafe before doing this!

For example, if we want to create a `WebClient` and our pipeline will download many different items through this, then process each differently:

```csharp

StartPipe<WebClient> getWebClient = new(()=>new WebClient());

getWebClient.Then(GetA).Then(ProcessA).Then(Save);
getWebClient.Then(GetB).Then(ProcessB).Then(Save);
getWebClient.Then(GetC).Then(ProcessC).Then(Save);

// Run any of these branches to execute everything
await getWebClient.Run();

```

Alternatively, you can make methods or properties that return branches, then combine with `And`

```csharp

[Then type] GetAndProcessA => new AsyncFunc(GetA).Then(ProcessA).Then(Save);
[Then type] GetAndProcessB => new AsyncFunc(GetB).Then(ProcessB).Then(Save);
[Then type] GetAndProcessC => new AsyncFunc(GetC).Then(ProcessB).Then(Save);


Task GetAndProcessAll() => new StartPipe(new WebClient).
                                Then(GetAndProcessA).
                                And(GetAndProcessB).
                                And(GetAndProcessC).
                                Run();

```

## Disclaimer

This library is built for my professional use with [Musink music software](https://musink.net), medical science, AI, and data science consulting. You're welcome to use, branch and extend it, so long as you stick within the license terms, but I cannot guarantee:
* Backwards compatibility as I make changes
* Timelines for specific updates
* Help with debugging.

Where I have contributed to a product or service, the most restrictive license terms take precedence.