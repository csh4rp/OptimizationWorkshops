using BenchmarkDotNet.Attributes;
using StructsCopyBenchmarkApp.Models;
using StructsCopyBenchmarkApp.Services;

namespace StructsCopyBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfIterations = 10000;
    
    [Benchmark(Description = "Class")]
    public void CalculateClass()
    {
        var dataFrame = new DataFrameClass { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateClass(dataFrame);
        }
    }
    
    [Benchmark(Description = "Class as Ref")]
    public void CalculateClassRef()
    {
        var dataFrame = new DataFrameClass { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateClassRef(in dataFrame);
        }
    }

    [Benchmark(Description = "Big Class")]
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

    [Benchmark(Description = "Big Class as Ref")]
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

    [Benchmark(Description = "Struct")]
    public void CalculateStruct()
    {
        var dataFrame = new DataFrameStruct { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
           _ = DataService.CalculateStruct(dataFrame);
        }
    }

    [Benchmark(Description = "Struct as Ref")]
    public void CalculateStructRef()
    {
        var dataFrame = new DataFrameStruct { X = 1, Y = 1 };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateStructRef(in dataFrame);
        }
    }

    [Benchmark(Description = "Big Struct")]
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

    [Benchmark(Description = "Big Struct as Ref")]
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
    
    [Benchmark(Description = "Struct as Interface")]
    public void CalculateStructAsInterface()
    {
        var dataFrame = new DataFrameStruct
        {
            X = 1,
            Y = 1,
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateInterFace(dataFrame);
        }
    }
    
    [Benchmark(Description = "Class as Interface")]
    public void CalculateClassAsInterface()
    {
        var dataFrame = new DataFrameClass
        {
            X = 1,
            Y = 1,
        };

        for (int i = 0; i < NumberOfIterations; i++)
        {
            _ = DataService.CalculateInterFace(dataFrame);
        }
    }
    
    [Benchmark(Description = "Big Struct as Interface")]
    public void CalculateBigStructAsInterface()
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
            _ = DataService.CalculateBigInterFace(dataFrame);
        }
    }
    
    [Benchmark(Description = "Big Class as Interface")]
    public void CalculateBigClassAsInterface()
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
            _ = DataService.CalculateBigInterFace(dataFrame);
        }
    }

}
