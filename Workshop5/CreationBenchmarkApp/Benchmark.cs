using BenchmarkDotNet.Attributes;

namespace CreationBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly List<int> Source = Enumerable.Range(0, 100_000)
        .OrderBy(_ => Random.Shared.Next())
        .ToList();

    [Params(10, 100, 1000, 10_000, 100_000)]
    public int NumberOfItems { get; set; }

    [Benchmark]
    public void CreateList()
    {
        var list = Source.Take(NumberOfItems).ToList();
    }
    
    [Benchmark]
    public void CreateSortedList()
    {
        var list = new SortedList<int, int>(NumberOfItems);
        for (var i = 0; i < NumberOfItems; i++)
        {
            var element = Source[i];
            list.Add(element, element);
        }
    }
    
    [Benchmark]
    public void CreateSet()
    {
        var set = Source.Take(NumberOfItems).ToHashSet();
    }
    
    [Benchmark]
    public void CreateSortedSet()
    {
        var set = new SortedSet<int>(Source.Take(NumberOfItems));
    }
 
    [Benchmark]
    public void CreateLinkedList()
    {
        var linkedList = new LinkedList<int>(Source.Take(NumberOfItems));
    }
    
    [Benchmark]
    public void CreateDictionary()
    {
        var dict = Source.Take(NumberOfItems).ToDictionary(k => k);
    }
    
    [Benchmark]
    public void CreateSortedDictionary()
    {
        var dict = new SortedDictionary<int, int>();
        for (var i = 0; i < NumberOfItems; i++)
        {
            var element = Source[i];
            dict.Add(element, element);
        }
    }
}
