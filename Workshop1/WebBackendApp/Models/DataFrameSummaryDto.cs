namespace WebBackendApp.Models;

public record DataFrameSummaryDto
{
    public required double XStdDev { get; init; }
    
    public required double YStdDev { get; init; }
    
    public required double ZStdDev { get; init; }
    
    public required long NumberOfDataFrames { get; init; }
}

