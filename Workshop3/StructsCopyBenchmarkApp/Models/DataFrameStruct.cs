namespace StructsCopyBenchmarkApp.Models;

public struct DataFrameStruct : IDataFrame
{
    public required double X { get; init; }
    
    public required double Y { get; init; }
}
