using BenchmarkDotNet.Attributes;

namespace RangeSearchingBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfElements = 100_000;
    
    private static readonly List<int> Source = Enumerable.Range(0, NumberOfElements)
        .OrderBy(_ => Random.Shared.Next())
        .ToList();

    private readonly SortedList<int, int> _sortedList = new();
    private readonly SortedSet<int> _sortedSet = new();
    private readonly List<int> _list = new();
    private readonly List<int> _listWithSortedItems = new();
    private readonly HashSet<int> _set = new();
    private readonly LinkedList<int> _linkedList = new();
    
    public Benchmark()
    {
        foreach (var i in Source)
        {
            _sortedSet.Add(i);
            _sortedList.Add(i, i);
            _list.Add(i);
            _set.Add(i);
            _linkedList.AddLast(i);
            _listWithSortedItems.Add(i);
        }
        
        _listWithSortedItems.Sort();
    }
    

    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInSortedList(Range range)
    {
        var items = new List<int>((range.End.Value - range.Start.Value) + 1);
        var startIndex = _sortedList.IndexOfKey(range.Start.Value);
        
        while (true)
        {
            var currentValue = _sortedList.Keys[startIndex++];

            if (currentValue <= range.End.Value)
            {
                items.Add(currentValue);
            }
            else
            {
                break;
            }
        }
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInSortedSet(Range range)
    {
        var values = _sortedSet.GetViewBetween(range.Start.Value, range.End.Value);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInList(Range range)
    {
        var values = _list
            .Where(i => i >= range.Start.Value && i <= range.End.Value)
            .Take((range.End.Value - range.Start.Value) + 1)
            .ToList();
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInListWithSortedItems(Range range)
    {
        var values = _listWithSortedItems
            .Where(i => i >= range.Start.Value && i <= range.End.Value)
            .Take((range.End.Value - range.Start.Value) + 1)
            .ToList();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInSet(Range range)
    {
        var values = _set
            .Where(i => i >= range.Start.Value && i <= range.End.Value)
            .Take((range.End.Value - range.Start.Value) + 1)
            .ToList();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Ranges))]
    public void FindInLinkedList(Range range)
    {
        var values = _linkedList
            .Where(i => i >= range.Start.Value && i <= range.End.Value)
            .Take((range.End.Value - range.Start.Value) + 1)
            .ToList();
    }

    public IEnumerable<object> Ranges()
    {
        // Find first
        yield return new Range(Source.First(), Source.First());
        
        // Find last
        yield return new Range(Source.Last(), Source.Last());
        
        yield return new Range(50_000, 50_010);
        
        yield return new Range(50_000, 60_000);
    }
}
