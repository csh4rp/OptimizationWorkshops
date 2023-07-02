using BenchmarkDotNet.Attributes;

namespace BenchmarkNetApp;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    private static readonly Random Random = new(0);

    [Benchmark(Baseline = true, Description = "Deterministic run")]
    public async Task RunAsync()
    {
        await Task.Delay(10);
    }

    [Benchmark(Description = "Random run")]
    public async Task RunRandomAsync()
    {
        await Task.Delay(Random.Next(0, 20));
    }
}
