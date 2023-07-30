using BenchmarkDotNet.Attributes;
using SearchingBenchmarkApp.Models;
using SearchingBenchmarkApp.Services;

namespace SearchingBenchmarkApp;

public class Benchmark
{
    private static readonly int[] Ids = 
    { 
        1, 7, 10, 12, 15, 17, 20, 59, 100, 123, 144, 200, 201, 204, 244, 256, 300, 399, 401, 540, 600, 700, 800, 900,
        901, 1000, 1001, 1002, 2003, 2100, 2201, 2500, 2757, 2800, 3010, 3111, 3456, 4154, 4567, 4975, 5314, 5464, 5799,
        6145, 6248, 6451, 7001, 7545, 7745, 7846, 7944, 8000, 8001, 8124, 8214, 8314, 8469, 8666, 8712, 8722, 8732, 8999,
        10000, 57999, 63000, 64012, 657845, 74300, 75322, 79777, 80000, 81011, 84632, 85444, 87999, 88456, 89666, 91000,
    };
    
    private static readonly IEnumerable<DataFrame> Data;

    [Params(10, 100, 1000, 10_000, 100_000)]
    public int Items { get; set; }
    
    static Benchmark()
    {
        Data = Enumerable.Range(1, 100_000).Select(d => new DataFrame
        {
            Id = d,
            Timestamp = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero).AddDays(d),
            X = Random.Shared.NextDouble(),
            Y = Random.Shared.NextDouble(),
            Z = Random.Shared.NextDouble()
        });
    }

    [Benchmark]
    public void RunList()
    {
        var service = new DataService(Data.Take(Items));

        _ = service.ProcessDataUsingList(Ids);
    }
    
    [Benchmark]
    public void RunDictionary()
    {
        var service = new DataService(Data.Take(Items));

        _ = service.ProcessDataUsingDictionary(Ids);
    }
}
