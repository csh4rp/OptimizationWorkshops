using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;
using OptimizationWorkshops.Workshop1.DotnetCounters;

var meter = new Meter("OptimizationWorkshops.Workshop1.DotnetCounters");
var counter = meter.CreateCounter<long>("Multiplication count");
var histogram = meter.CreateHistogram<double>("Multiplication time in nanoseconds");

var iterationNumber = 0L;
while (true)
{
    var list = new List<Data>(1000);

    for (var i = iterationNumber; i < iterationNumber + 1000; i++)
    {
        list.Add(new Data
        {
            W = i,
            X = 1.1 * i,
            Y = 1.2 * i,
            Z = 1.3 * i
        });
    }

    var timestamp = Stopwatch.GetTimestamp();
    var result = new double[list.Count * 4];

    for (var i = 0; i < list.Count - 1; i++)
    {
        var firstItem = list[i];
        var secondItem = list[i + 1];

        var firstVector = new Vector<double>(new[] { firstItem.W, firstItem.X, firstItem.Y, firstItem.Z});
        var secondVector = new Vector<double>(new[] { secondItem.W, secondItem.X, secondItem.Y, secondItem.Z});

        (firstVector * secondVector).CopyTo(result, i * 4);

        counter.Add(1);
    }

    histogram.Record(Stopwatch.GetElapsedTime(timestamp).TotalNanoseconds);

    iterationNumber++;
}
