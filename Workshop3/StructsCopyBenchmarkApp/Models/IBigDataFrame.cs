namespace StructsCopyBenchmarkApp.Models;

public interface IBigDataFrame
{
    public DateTimeOffset Timestamp { get; init; }
    
    public decimal X { get; init; }
    
    public decimal Y { get; init; }
    
    public decimal Z { get; init; }
    
    public decimal Avg { get; init; }
    
    public decimal Med { get; init; }
}
