using BenchmarkDotNet.Attributes;
using IterationBenchmarkApp.Models;
using Microsoft.VisualBasic;

namespace IterationBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly int[] BatchSizes = {10, 100, 1000, 10_000, 100_000};
    
    [Benchmark(Description = "Enumerable - using foreach")]
    [ArgumentsSource(nameof(Enumerables))]
    public void IterateOverEnumerable(IEnumerable<DataFrame> dataFrames)
    {
        DataFrame value;
        
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "List - using foreach")]
    [ArgumentsSource(nameof(Lists))]
    public void IterateOverListForeach(List<DataFrame> dataFrames)
    {
        DataFrame value;
        
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "List - using for")]
    [ArgumentsSource(nameof(Lists))]
    public void IterateOverListFor(List<DataFrame> dataFrames)
    {
        DataFrame value;

        for (var i = 0; i < dataFrames.Count; i++)
        {
            value = dataFrames[i];
        }
    }
    
    [Benchmark(Description = "List as IEnumerable - using foreach")]
    [ArgumentsSource(nameof(Lists))]
    public void IterateListAsEnumerable(IEnumerable<DataFrame> dataFrames)
    {
        DataFrame value;
        
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
        
    [Benchmark(Description = "List as IReadOnlyCollection - using foreach")]
    [ArgumentsSource(nameof(Lists))]
    public void IterateAsIReadonlyCollection(IEnumerable<DataFrame> dataFrames)
    {
        DataFrame value;

        var collection = dataFrames as IReadOnlyCollection<DataFrame> ?? dataFrames.ToList();
        
        foreach (var item in collection)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "Set - using foreach")]
    [ArgumentsSource(nameof(Sets))]
    public void IterateOverSet(HashSet<DataFrame> set)
    {
        DataFrame value;
        
        foreach (var item in set)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "Queue - using foreach")]
    [ArgumentsSource(nameof(Queues))]
    public void IterateOverQueueForeach(Queue<DataFrame> dataFrames)
    {
        DataFrame value;
        
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "Queue - using dequeue")]
    [ArgumentsSource(nameof(Queues))]
    public void IterateOverQueueDequeue(Queue<DataFrame> dataFrames)
    {
        DataFrame? value;

        while (dataFrames.TryDequeue(out value))
        {
            
        }
    }
    
    [Benchmark(Description = "Stack - using foreach")]
    [ArgumentsSource(nameof(Stacks))]
    public void IterateOverStackForeach(Stack<DataFrame> dataFrames)
    {
        DataFrame value;
        
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "Stack - using pop")]
    [ArgumentsSource(nameof(Stacks))]
    public void IterateOverStackPop(Stack<DataFrame> dataFrames)
    {
        DataFrame? value;

        while (dataFrames.TryPop(out value))
        {
            
        }
    }
    
    [Benchmark(Description = "LinkedList - using foreach")]
    [ArgumentsSource(nameof(LinkedLists))]
    public void IterateOverLinkedListForeach(LinkedList<DataFrame> dataFrames)
    {
        DataFrame value;
        foreach (var item in dataFrames)
        {
            value = item;
        }
    }
    
    [Benchmark(Description = "LinkedList - using next")]
    [ArgumentsSource(nameof(LinkedLists))]
    public void IterateOverLinkedListNext(LinkedList<DataFrame> dataFrames)
    {
        DataFrame value;

        var node = dataFrames.First;
        while (node != null)
        {
            value = node.Value;
            node = node.Next;
        }
    }
    
    public IEnumerable<object> Lists()
    {
        var list = Source().ToList();

        foreach (var size in BatchSizes)
        {
            yield return list.Take(size).ToList();
        }
    }
    
    public IEnumerable<object> LinkedLists()
    {
        var list = Source().ToList();

        foreach (var size in BatchSizes)
        {
            var linkedList = new LinkedList<DataFrame>();
            for (var i = 0; i < size; i++)
            {
                linkedList.AddLast(list[i]);
            }

            yield return linkedList;
        }
    }
    
    public IEnumerable<object> Sets()
    {
        var list = Source().ToList();

        foreach (var size in BatchSizes)
        {
            var set = new HashSet<DataFrame>();
            for (var i = 0; i < size; i++)
            {
                set.Add(list[i]);
            }
            
            yield return set;
        }
    }
    
    public IEnumerable<object> Queues()
    {
        var list = Source().ToList();

        foreach (var size in BatchSizes)
        {
            var queue = new Queue<DataFrame>();
            for (var i = 0; i < size; i++)
            {
                queue.Enqueue(list[i]);
            }

            yield return queue;
        }
    }
    
    public IEnumerable<object> Stacks()
    {
        var list = Source().ToList();

        foreach (var size in BatchSizes)
        {
            var stack = new Stack<DataFrame>();
            for (var i = 0; i < size; i++)
            {
                stack.Push(list[i]);
            }

            yield return stack;
        }
    }
    
    public IEnumerable<object> Enumerables()
    {
        var source = Source();

        foreach (var size in BatchSizes)
        {
            yield return source.Take(size);
        }
    }

    private static IEnumerable<DataFrame> Source() => Enumerable.Range(1, 100_000)
        .Select(i => new DataFrame
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTimeOffset.UtcNow,
            X = Random.Shared.NextDouble(),
            Y = Random.Shared.NextDouble(),
            Z = Random.Shared.NextDouble()
        });
}
