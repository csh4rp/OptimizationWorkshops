using BenchmarkDotNet.Attributes;

namespace ThreadBenchmarkApp;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    [Benchmark]
    public void RunThread()
    {
        var threads = new List<Thread>();

        for (int i = 0; i < 100; i++)
        {
            var thread = new Thread(_ =>
            {
                Thread.Sleep(100);
            });

            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
    
    [Benchmark]
    public async Task RunTask()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 100; i++)
        {
            var task = new Task(() =>
            {
                Thread.Sleep(100);
            });

            task.Start();
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task RunTaskWithMinThreads()
    {
        ThreadPool.SetMinThreads(100, 100);
        var tasks = new List<Task>();
        
        for (int i = 0; i < 100; i++)
        {
            var task = new Task(() =>
            {
                Thread.Sleep(100);
            });

            task.Start();
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
    }
}
