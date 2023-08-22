using BenchmarkDotNet.Attributes;

namespace ArraySlicingBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly DataFrame[] DataFrames = new DataFrame[262144];

    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int ConcurrencyLevel { get; set; }
    
    static Benchmark()
    {
        for (var i = 0; i < DataFrames.Length; i++)
        {
            DataFrames[i] = new DataFrame
            {
                Timestamp = DateTimeOffset.UtcNow,
                X = Random.Shared.NextDouble(),
                Y = Random.Shared.NextDouble(),
                Z = Random.Shared.NextDouble()
            };
        }
    }
    
    [Benchmark]
    public async Task RunArray()
    {
        var chunkSize = DataFrames.Length / ConcurrencyLevel;

        var startIndexes = Enumerable.Range(0, ConcurrencyLevel)
            .Select(i => i * chunkSize);
        
        var tasks = startIndexes.Select(idx => Task.Run(() =>
        {
            double x = 0, y = 0, z = 0; 
            
            for (var i = idx; i < idx + chunkSize; i++)
            {
                var frame = DataFrames[i];
                x += frame.X;
                y += frame.Y;
                z += frame.Z;
            }

            return (X: x, Y: y, Z: z);
        })).ToArray();

        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task RunArraySegment()
    {
        var chunkSize = DataFrames.Length / ConcurrencyLevel;

        var chunks = Enumerable.Range(0, ConcurrencyLevel)
            .Select(i => new ArraySegment<DataFrame>(DataFrames, i * chunkSize, chunkSize));

        var tasks = chunks.Select(chunk => Task.Run(() =>
        {
            double x = 0, y = 0, z = 0; 
            
            for (var i = 0; i < chunk.Count; i++)
            {
                var frame = chunk[i];
                x += frame.X;
                y += frame.Y;
                z += frame.Z;
            }

            return (X: x, Y: y, Z: z);
        })).ToArray();

        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task RunArrayChunks()
    {
        var chunks = DataFrames.Chunk(DataFrames.Length / ConcurrencyLevel);

        var tasks = chunks.Select(chunk => Task.Run(() =>
        {
            double x = 0, y = 0, z = 0; 
            
            for (var i = 0; i < chunk.Length; i++)
            {
                var frame = chunk[i];
                x += frame.X;
                y += frame.Y;
                z += frame.Z;
            }

            return (X: x, Y: y, Z: z);
        })).ToArray();

        await Task.WhenAll(tasks);
    }
}
