using BenchmarkDotNet.Attributes;

namespace SearchingBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfElements = 100_000;

    private static readonly List<int> Source = Enumerable.Range(0, NumberOfElements)
        // .OrderBy(i => Random.Shared.Next())
        .ToList();
    
    private readonly SortedList<int, int> _sortedList = new();
    private readonly SortedSet<int> _sortedSet = new();
    private readonly List<int> _list = new();
    private readonly HashSet<int> _set = new();
    private readonly LinkedList<int> _linkedList = new();
    
    public Benchmark()
    {
        foreach(var i in Source)
        {
            _sortedList.Add(i, i);
            _sortedSet.Add(i);
            _set.Add(i);
            _linkedList.AddLast(i);
        }

        _list = Source;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Items))]
    public void FindInSortedList(int itemToFind)
    {
        _sortedList.TryGetValue(itemToFind, out var item);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Items))]
    public void FindInSortedSet(int itemToFind)
    {
        _sortedSet.TryGetValue(itemToFind, out var item);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Items))]
    public void FindInList(int itemToFind)
    {
        int item;
        
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == itemToFind)
            {
                item = _list[i];
                break;
            }
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(Items))]
    public void FindInSet(int itemToFind)
    {
        _ = _set.TryGetValue(itemToFind, out var item);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Items))]
    public void FindInLinkedList(int itemToFind)
    {
        var item = _linkedList.Find(itemToFind);
    }

    public IEnumerable<object> Items()
    {
        // Find first element
        yield return Source[0];
        
        // Find element in the middle
        yield return Source[NumberOfElements / 2];
        
        // Find last element
        yield return Source[NumberOfElements - 1];
    }
}
