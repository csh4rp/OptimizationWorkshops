using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace PoolingBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    [Params(10, 100, 1000, 10_000, 100_000)]
    public int NumberOfItems { get; set; }
    
    [Benchmark]
    public void RunPool()
    {
        var arr = ArrayPool<long>.Shared.Rent(NumberOfItems);

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr[i] = i;
        }
    }
    
    [Benchmark]
    public void RunPoolWithReturn()
    {
        var arr = ArrayPool<long>.Shared.Rent(NumberOfItems);

        for (var i = 0; i < NumberOfItems; i++)
        {
            arr[i] = i;
        }
        
        ArrayPool<long>.Shared.Return(arr);
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
