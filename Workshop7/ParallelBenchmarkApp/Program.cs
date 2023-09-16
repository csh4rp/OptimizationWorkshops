using BenchmarkDotNet.Running;
using ParallelBenchmarkApp;

// BenchmarkRunner.Run<Benchmark>();

await new Benchmark().RunTasksWithSeparateSum();
