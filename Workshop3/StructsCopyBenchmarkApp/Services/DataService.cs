using System.Runtime.CompilerServices;
using StructsCopyBenchmarkApp.Models;

namespace StructsCopyBenchmarkApp.Services;

public class DataService
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double CalculateClass(DataFrameClass dataFrame) => dataFrame.X + dataFrame.Y;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double CalculateClassRef(in DataFrameClass dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static decimal CalculateBigClass(BigDataFrameClass dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static decimal CalculateBigClassRef(in BigDataFrameClass dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double CalculateStruct(DataFrameStruct dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double CalculateStructRef(in DataFrameStruct dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static decimal CalculateBigStruct(BigDataFrameStruct dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static decimal CalculateBigStructRef(in BigDataFrameStruct dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double CalculateInterFace(IDataFrame dataFrame) => dataFrame.X + dataFrame.Y;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static decimal CalculateBigInterFace(IBigDataFrame dataFrame) => dataFrame.X + dataFrame.Y;
}
