using AsyncBenchmarkApp.Services;
using BenchmarkDotNet.Attributes;

namespace AsyncBenchmarkApp;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    [Benchmark]
    public async Task RunFinishedTask()
    {
        _ = await DataService.RunFinishedTask();
    }
    
    [Benchmark]
    public async Task RunTaskYield()
    {
        _ = await DataService.RunTaskYield();
    }

    [Benchmark]
    public async Task RunAwaitCompletedTask()
    {
        _ = await DataService.RunAwaitCompletedTask();
    }
    
    [Benchmark]
    public async Task RunAwaitCompletedValueTask()
    {
        _ = await DataService.RunAwaitCompletedValueTask();
    }
    
    [Benchmark]
    public async Task RunCachedTask()
    {
        _ = await DataService.RunCachedTask();
    }
    
    [Benchmark]
    public async Task RunFinishedValueTask()
    {
        _ = await DataService.RunFinishedValueTask();
    }
    
    [Benchmark]
    public async Task RunValueTaskYield()
    {
        _ = await DataService.RunValueTaskYield();
    }
}
