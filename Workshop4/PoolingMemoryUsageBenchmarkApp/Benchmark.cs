using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace PoolingMemoryUsageBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfIterations = 1000;
    
    [Params(10, 100, 1000, 10_000, 100_000)]
    public int ArraySize { get; set; }
    
    [Benchmark]
    public void AllocatePool()
    {
        for (var i = 0; i < NumberOfIterations; i++)
        {
            var array = ArrayPool<int>.Shared.Rent(ArraySize);
            array[0] = 1;
        }
    }
    
    [Benchmark]
    public void AllocatePoolWithReturn()
    {
        for (var i = 0; i < NumberOfIterations; i++)
        {
            var array = ArrayPool<int>.Shared.Rent(ArraySize);
            array[0] = 1;
            ArrayPool<int>.Shared.Return(array);
        }
    }
    
    [Benchmark]
    public void Allocate()
    {
        for (var i = 0; i < NumberOfIterations; i++)
        {
            var array = new int[ArraySize];
            array[0] = 1;
        }
    }
}
