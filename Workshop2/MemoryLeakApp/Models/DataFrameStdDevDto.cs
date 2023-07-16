namespace MemoryLeakApp.Models;

public record DataFrameStdDevDto
{
    public required double XStdDev { get; init; }
    
    public required double YStdDev { get; init; }
    
    public required double ZStdDev { get; init; }
}
