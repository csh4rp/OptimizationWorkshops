using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace PoolingBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfItems = 1000;
    
    [Benchmark]
    public void RunPoll()
    {
        var arr = ArrayPool<long>.Shared.Rent(NumberOfItems);

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr[i] = i;
        }
    }
    
    [Benchmark]
    public void RunNotInitialized()
    {
        var arr = new List<long>();

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr.Add(i);
        }
    }
    
    [Benchmark]
    public void RunInitialized()
    {
        var arr = new List<long>(NumberOfItems);

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr.Add(i);
        }
    }
    
    [Benchmark]
    public void RunStack()
    {
        Span<long> arr = stackalloc long[NumberOfItems];

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr[i] = i;
        }
    }
}
