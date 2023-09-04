using BenchmarkDotNet.Attributes;
using EnumerableBenchmarkApp.Models;
using EnumerableBenchmarkApp.Services;

namespace EnumerableBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly IEnumerable<DataFrame> Source = Enumerable.Range(1, 100_000)
        .Select(i => new DataFrame
        {
            Id = i,
            Timestamp = new DateTimeOffset(2000, 1, 1, 12, 0, 0, TimeSpan.Zero).AddHours(i),
            X = Random.Shared.NextDouble(),
            Y = Random.Shared.NextDouble(),
            Z = Random.Shared.NextDouble(),
        }).ToList();

    private static Func<DataFrame, bool> Filter { get; } = x =>
        x.Timestamp > new DateTimeOffset(2005, 6, 1, 12, 0, 0, TimeSpan.Zero);
    
    [Benchmark(Description = "foreach")]
    public void RunUsingForeach()
    {
        var result = ProcessingService.ProcessUsingForeach(Source);
    }
    
    [Benchmark(Description = "foreach - where")]
    public void RunUsingForeachAndWhere()
    {
        var items = Source.Where(Filter);
        
        var result = ProcessingService.ProcessUsingForeach(items);
    }
    
    [Benchmark(Description = "LINQ")]
    public void RunUsingLinq()
    {
        var result = ProcessingService.ProcessUsingLinq(Source);
    }
    
    [Benchmark(Description = "LINQ - where")]
    public void RunUsingLinqAndWhere()
    {
        var items = Source.Where(Filter);
        
        var result = ProcessingService.ProcessUsingLinq(items);
    }
    
    [Benchmark(Description = "LINQ with cast")]
    public void RunUsingLinqWithCast()
    {
        var result = ProcessingService.ProcessUsingLinqWithCast(Source);
    }
    
    [Benchmark(Description = "LINQ with cast - where")]
    public void RunUsingLinqWithCastAndWhere()
    {
        var items = Source.Where(Filter);
        
        var result = ProcessingService.ProcessUsingLinqWithCast(items);
    }
    
    [Benchmark(Description = "LINQ with ToList")]
    public void RunUsingLinqWithToList()
    {
        var result = ProcessingService.ProcessUsingLinqWithToList(Source);
    }
    
    [Benchmark(Description = "LINQ with ToList - where")]
    public void RunUsingLinqWithToListAndWhere()
    {
        var items = Source.Where(Filter);
        
        var result = ProcessingService.ProcessUsingLinqWithToList(items);
    }
}
