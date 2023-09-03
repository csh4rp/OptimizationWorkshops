using BenchmarkDotNet.Attributes;

namespace EnumerableBenchmark;

[MemoryDiagnoser]
public class Benchmark
{
    public Benchmark()
    {
        var q = new Queue<int>();
        q.Enqueue(1);
    }
}
