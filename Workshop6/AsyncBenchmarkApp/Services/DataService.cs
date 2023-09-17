namespace AsyncBenchmarkApp.Services;

public static class DataService
{
    public static async Task<int> RunTaskAsync()
    {
        await Task.Yield();
        return 1;
    }

    public static Task<int> RunTask() => Task.FromResult(1);
    
    public static async ValueTask<int> RunValueTaskAsync()
    {
        await Task.Yield();
        return 1;
    }

    public static ValueTask<int> RunValueTask() => new(1);
    
}
