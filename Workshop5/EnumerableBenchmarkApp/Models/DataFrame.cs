﻿namespace EnumerableBenchmarkApp.Models;

public record DataFrame
{
    public required long Id { get; init; }
    
    public required DateTimeOffset Timestamp { get; init; }
    
    public required double X { get; init; }
    
    public required double Y { get; init; }
    
    public required double Z { get; init; }
}
