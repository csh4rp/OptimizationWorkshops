namespace WebBackend.Models;

public record DataFrameSummaryDto
{
    public required double XStdDev { get; init; }
    
    public required double YStdDev { get; init; }
    
    public required double ZStdDev { get; init; }
    
    public required double X99Percentile { get; init; }
    
    public required double X95Percentile { get; init; }
    
    public required double X90Percentile { get; init; }
    
    public required double X75Percentile { get; init; }
    
    public required double X50Percentile { get; init; }
    
    public required double Y99Percentile { get; init; }
    
    public required double Y95Percentile { get; init; }
    
    public required double Y90Percentile { get; init; }
    
    public required double Y75Percentile { get; init; }
    
    public required double Y50Percentile { get; init; }
    
    public required double Z99Percentile { get; init; }
    
    public required double Z95Percentile { get; init; }
    
    public required double Z90Percentile { get; init; }
    
    public required double Z75Percentile { get; init; }
    
    public required double Z50Percentile { get; init; }
    
    public required long NumberOfDataFrames { get; init; }
}

