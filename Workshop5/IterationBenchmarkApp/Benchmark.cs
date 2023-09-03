using BenchmarkDotNet.Attributes;
using IterationBenchmarkApp.Models;

namespace IterationBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private readonly IEnumerable<DataFrame> _enumerable;
    private readonly IEnumerable<DataFrame> _enumerableList;
    private readonly List<DataFrame> _list = new();
    private readonly HashSet<DataFrame> _set = new();
    private readonly LinkedList<DataFrame> _linkedList = new();
    private readonly Queue<DataFrame> _queue = new();
    private readonly Stack<DataFrame> _stack = new();

    public Benchmark()
    {
        var source = Enumerable.Range(1, 10_000)
            .Select(i => new DataFrame
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTimeOffset.UtcNow,
                X = Random.Shared.NextDouble(),
                Y = Random.Shared.NextDouble(),
                Z = Random.Shared.NextDouble()
            });
        
        foreach (var dataFrame in source)
        {
            _list.Add(dataFrame);
            _linkedList.AddLast(dataFrame);
            _set.Add(dataFrame);
            _queue.Enqueue(dataFrame);
            _stack.Push(dataFrame);
        }
        
        _enumerable = source;
        _enumerableList = _list;
    }
    
    [Benchmark]
    public void IterateOverEnumerable()
    {
        DataFrame value;
        
        foreach (var item in _enumerable)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverList()
    {
        DataFrame value;
        
        foreach (var item in _list)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateListAsEnumerable()
    {
        DataFrame value;
        
        foreach (var item in _enumerableList)
        {
            value = item;
        }
    }
    
        
    [Benchmark]
    public void IterateAsIReadonlyCollection()
    {
        DataFrame value;

        var collection = _enumerableList as IReadOnlyCollection<DataFrame> ?? _enumerableList.ToList();
        
        foreach (var item in collection)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverSet()
    {
        DataFrame value;
        
        foreach (var item in _set)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverQueue()
    {
        DataFrame value;
        
        foreach (var item in _queue)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverStack()
    {
        DataFrame value;
        
        foreach (var item in _stack)
        {
            value = item;
        }
    }
    
    [Benchmark]
    public void IterateOverLinkedList()
    {
        DataFrame value;
        
        foreach (var item in _linkedList)
        {
            value = item;
        }
    }
}
