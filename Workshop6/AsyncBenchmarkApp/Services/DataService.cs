namespace AsyncBenchmarkApp.Services;

public static class DataService
{
    public static async Task<int> RunTaskYield()
    {
        await Task.Yield();
        return 100;
    }
    
    public static async Task<int> RunAwaitCompletedTask()
    {
        await Task.CompletedTask;
        return 100;
    }

    public static Task<int> RunFinishedTask() => Task.FromResult(100);
    
    public static Task<int> RunCachedTask() => Task.FromResult(1);
    
    public static async ValueTask<int> RunValueTaskYield()
    {
        await Task.Yield();
        return 100;
    }

    public static ValueTask<int> RunFinishedValueTask() => new(100);
    
    public static async ValueTask<int> RunAwaitCompletedValueTask()
    {
        await Task.CompletedTask;
        return 100;
    }
    
}
