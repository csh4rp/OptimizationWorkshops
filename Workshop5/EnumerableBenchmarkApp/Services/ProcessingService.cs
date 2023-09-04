using EnumerableBenchmarkApp.Models;

namespace EnumerableBenchmarkApp.Services;

public class ProcessingService
{
    public static (double X, double Y, double Z) ProcessUsingForeach(IEnumerable<DataFrame> dataFrames)
    {
        double xSum = 0, ySum = 0, zSum = 0;

        foreach (var dataFrame in dataFrames)
        {
            xSum += dataFrame.X;
            ySum += dataFrame.Y;
            zSum += dataFrame.Z;
        }

        return (xSum, ySum, zSum);
    }
    
    public static (double X, double Y, double Z) ProcessUsingLinq(IEnumerable<DataFrame> dataFrames)
    {
        var xSum = dataFrames.Sum(x => x.X);
        var ySum = dataFrames.Sum(x => x.Y);
        var zSum = dataFrames.Sum(x => x.Z);
        
        return (xSum, ySum, zSum);
    }
    
    public static (double X, double Y, double Z) ProcessUsingLinqWithCast(IEnumerable<DataFrame> dataFrames)
    {
        var frames = dataFrames as IReadOnlyCollection<DataFrame> ?? dataFrames.ToList();
        
        var xSum = frames.Sum(x => x.X);
        var ySum = frames.Sum(x => x.Y);
        var zSum = frames.Sum(x => x.Z);
        
        return (xSum, ySum, zSum);
    }
    
    public static (double X, double Y, double Z) ProcessUsingLinqWithToList(IEnumerable<DataFrame> dataFrames)
    {
        var frames = dataFrames.ToList();
        
        var xSum = frames.Sum(x => x.X);
        var ySum = frames.Sum(x => x.Y);
        var zSum = frames.Sum(x => x.Z);
        
        return (xSum, ySum, zSum);
    }
}
