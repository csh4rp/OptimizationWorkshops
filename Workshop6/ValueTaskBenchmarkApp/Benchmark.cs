using BenchmarkDotNet.Attributes;
using ValueTaskBenchmarkApp.Services;

namespace ValueTaskBenchmarkApp;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchmark
{
    [Benchmark]
    public async Task RunTaskCached()
    {
        _ = await DataService.RunTask(false);
    }

    [Benchmark]
    public async Task RunTaskUncached()
    {
        _ = await DataService.RunTask(true);
    }

    [Benchmark]
    public async Task RunValueTaskCached()
    {
        _ = await DataService.RunValueTask(false);
    }

    [Benchmark]
    public async Task RunValueTaskUncached()
    {
        _ = await DataService.RunValueTask(true);
    }
}
