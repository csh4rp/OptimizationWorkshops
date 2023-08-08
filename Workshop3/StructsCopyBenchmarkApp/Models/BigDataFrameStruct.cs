namespace StructsCopyBenchmarkApp.Models;

public struct BigDataFrameStruct : IBigDataFrame
{
    public required DateTimeOffset Timestamp { get; init; }
    
    public required decimal X { get; init; }
    
    public required decimal Y { get; init; }
    
    public required decimal Z { get; init; }
    
    public required decimal Avg { get; init; }
    
    public required decimal Med { get; init; }
}
