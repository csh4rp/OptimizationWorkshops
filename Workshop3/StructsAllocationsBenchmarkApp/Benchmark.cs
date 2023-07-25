using BenchmarkDotNet.Attributes;
using StructsAllocationsBenchmarkApp.Models;

namespace StructsAllocationsBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const int NumberOfItems = 10_000;
    
    [Benchmark]
    public void AllocateClass()
    {
        var items = new List<DataFrameClass>();

        for (int i = 0; i < NumberOfItems; i++)
        {
            items.Add(new DataFrameClass
            {
                X = 1,
                Y = 1,
                Z = 1
            });
        }
    } 
    
    [Benchmark]
    public void AllocateStruct()
    {
        var items = new List<DataFrameStruct>();

        for (int i = 0; i < NumberOfItems; i++)
        {
            items.Add(new DataFrameStruct()
            {
                X = 1,
                Y = 1,
                Z = 1
            });
        }
    } 
    
    [Benchmark]
    public void AllocateStack()
    {
        Span<DataFrameStruct> items = stackalloc DataFrameStruct[NumberOfItems];

        for (int i = 0; i < NumberOfItems; i++)
        {
            items[i] = new DataFrameStruct { X = 1, Y = 1, Z = 1 };
        }
    } 

}
