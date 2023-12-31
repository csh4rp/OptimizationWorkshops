﻿namespace StructsAllocationsBenchmarkApp.Models;

public struct DataFrameStruct
{
    public required DateTimeOffset Timestamp { get; init; }
    
    public required double X { get; init; }
    
    public required double Y { get; init; }
    
    public required double Z { get; init; }
}
