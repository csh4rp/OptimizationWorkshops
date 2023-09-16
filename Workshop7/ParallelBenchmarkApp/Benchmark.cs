using BenchmarkDotNet.Attributes;

namespace ParallelBenchmarkApp;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    private static readonly int[] Items = new int[100000];

    static Benchmark()
    {
        for (var i = 0; i < Items.Length; i++)
        {
            Items[i] = i;
        }
    }
    
    [Benchmark]
    public void RunParallelFor()
    {
        var sum = 0L;
        
        Parallel.For(0, Items.Length, s =>
        {
            Interlocked.Add(ref sum, s);
        });
    }
    
    [Benchmark]
    public void RunParallelForEach()
    {
        var sum = 0L;

        Parallel.ForEach(Items, s =>
        {
            Interlocked.Add(ref sum, s);
        });
    }

    [Benchmark]
    public async Task RunTasks()
    {
        var sum = 0L;
        var tasks = new List<Task>();
        var parallelWorkers = Environment.ProcessorCount;
        var itemsPerTask = Items.Length / parallelWorkers;

        for (var i = 0; i < parallelWorkers; i++)
        {
            var index = i;
            tasks.Add(Task.Run(() =>
            {
                var arr = new ArraySegment<int>(Items, index * itemsPerTask, itemsPerTask);

                foreach (var s in arr)
                {
                    Interlocked.Add(ref sum, s);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task RunTasksWithSeparateSum()
    {
        var sum = 0L;
        var tasks = new List<Task<long>>();
        var parallelWorkers = Environment.ProcessorCount;
        var itemsPerTask = Items.Length / parallelWorkers;

        for (var i = 0; i < parallelWorkers; i++)
        {
            var index = i;
            tasks.Add(Task.Run(() =>
            {
                var localSum = 0L;
                var arr = new ArraySegment<int>(Items, index * itemsPerTask, itemsPerTask);

                foreach (var s in arr)
                {
                    localSum += s;
                }

                return localSum;
            }));
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            sum += task.Result;
        }
    }
}
