namespace ValueTaskBenchmarkApp.Services;

public static class DataService
{
    private static readonly Func<Task<int>> DataFetcher = () => Task.Run(async () =>
    {
        await Task.Yield();
        return 100;
    });

    public static Task<int> RunTask(bool runAsync) => runAsync ? DataFetcher() : Task.FromResult(100);

    public static ValueTask<int> RunValueTask(bool runAsync) => 
        runAsync ? new ValueTask<int>(DataFetcher()) : new ValueTask<int>(100);
}
