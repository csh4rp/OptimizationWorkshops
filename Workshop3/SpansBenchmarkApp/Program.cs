using BenchmarkDotNet.Running;
using SpansBenchmarkApp;

// BenchmarkRunner.Run<Benchmark>();

new Benchmark().RunSpan();
new Benchmark().RunSubstring();
new Benchmark().RunStringSplit();
