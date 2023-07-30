using BenchmarkDotNet.Attributes;

namespace IterationBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly IEnumerable<int> Enumerable;
    private static readonly IEnumerable<int> EnumerableList;
    private static readonly List<int> List;

    static Benchmark()
    {
        Enumerable = System.Linq.Enumerable.Range(1, 1000);
        List = Enumerable.ToList();
        EnumerableList = List;
    }
    
    [Benchmark]
    public void IterateOverEnumerable()
    {
        int value;
        
        foreach (var item in Enumerable)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverList()
    {
        int value;
        
        foreach (var item in List)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateListAsEnumerable()
    {
        int value;
        
        foreach (var item in EnumerableList)
        {
            value = item;
        }
    }
}
