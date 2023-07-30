using BenchmarkDotNet.Running;
using StructsAllocationsBenchmarkApp;

using var init = new DbInitializer();
await init.InitializeAsync();

BenchmarkRunner.Run<Benchmark>();
