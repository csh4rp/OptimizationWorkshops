using AsyncBenchmarkApp.Services;
using BenchmarkDotNet.Attributes;

namespace AsyncBenchmarkApp;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    [Benchmark]
    public async Task RunTask()
    {
        _ = await DataService.RunTask();
    }

    [Benchmark]
    public async Task RunTaskAsync()
    {
        _ = await DataService.RunTaskAsync();
    }

    [Benchmark]
    public async Task RunValueTask()
    {
        _ = await DataService.RunValueTask();
    }

    [Benchmark]
    public async Task RunValueTaskAsync()
    {
        _ = await DataService.RunValueTaskAsync();
    }
}
