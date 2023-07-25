using BenchmarkDotNet.Attributes;
using StructsCopyBenchmarkApp.Models;
using StructsCopyBenchmarkApp.Services;

namespace StructsCopyBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfIterations = 10000;
    
    [Benchmark()]
    public void CalculateClass()
    {
        var dataFrame = new DataFrameClass { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateClass(dataFrame);
        }
    }
    
    [Benchmark()]
    public void CalculateClassRef()
    {
        var dataFrame = new DataFrameClass { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateClassRef(in dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateBigClass()
    {
        var dataFrame = new BigDataFrameClass
        {
            X = 1,
            Y = 1,
            Z = 1,
            Avg = 1,
            Med = 1,
            Timestamp = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero)
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateBigClass(dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateBigClassRef()
    {
        var dataFrame = new BigDataFrameClass
        {
            X = 1,
            Y = 1,
            Z = 1,
            Avg = 1,
            Med = 1,
            Timestamp = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero)
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateBigClassRef(in dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateStruct()
    {
        var dataFrame = new DataFrameStruct { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            DataService.CalculateStruct(dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateStructRef()
    {
        var dataFrame = new DataFrameStruct { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateStructRef(in dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateBigStruct()
    {
        var dataFrame = new BigDataFrameStruct
        {
            X = 1,
            Y = 1,
            Z = 1,
            Avg = 1,
            Med = 1,
            Timestamp = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero)
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateBigStruct(dataFrame);
        }
    }

    [Benchmark()]
    public void CalculateBigStructRef()
    {
        var dataFrame = new BigDataFrameStruct
        {
            X = 1,
            Y = 1,
            Z = 1,
            Avg = 1,
            Med = 1,
            Timestamp = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero)
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateBigStructRef(in dataFrame);
        }
    }

}
