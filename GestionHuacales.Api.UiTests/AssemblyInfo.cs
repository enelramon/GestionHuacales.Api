using NUnit.Framework;

// Enable parallel execution at the assembly level
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Set the number of workers (optional - defaults to number of CPU cores)
[assembly: LevelOfParallelism(4)]