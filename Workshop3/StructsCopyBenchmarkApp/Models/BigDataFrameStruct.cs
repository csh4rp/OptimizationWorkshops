﻿namespace StructsCopyBenchmarkApp.Models;

public struct BigDataFrameStruct
{
    public DateTimeOffset Timestamp { get; init; }
    
    public required double X { get; init; }
    
    public required double Y { get; init; }
    
    public required double Z { get; init; }
    
    public required double Avg { get; init; }
    
    public required double Med { get; init; }

}
