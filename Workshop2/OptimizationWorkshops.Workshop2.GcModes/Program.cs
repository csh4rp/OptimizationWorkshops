using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using OptimizationWorkshops.Workshop2.GcModes;

var meter = new Meter("OptimizationWorkshops.Workshop2.GcModes");
var counter = meter.CreateCounter<long>("Operation count");

var concurrentQueue = new ConcurrentQueue<DataFrame>();

var producerTasks = Enumerable.Range(1, Environment.ProcessorCount * 16)
    .Select(_ => Task.Run(async () =>
    {
        while (true)
        {
            var dataFrame = new DataFrame(DateTimeOffset.UtcNow, Random.Shared.NextDouble(), Random.Shared.NextDouble(), Random.Shared.NextDouble());
            concurrentQueue.Enqueue(dataFrame);
            counter.Add(1);
            await Task.Delay(1);
        }
    }))
    .ToArray();


var consumerTasks = Enumerable.Range(1, Environment.ProcessorCount * 8)
    .Select(_ => Task.Run(async () =>
    {
        while (true)
        {
            if (concurrentQueue.TryDequeue(out var dataFrame))
            {
                Console.WriteLine(dataFrame);
                
                // Fake some work
                await Task.Delay(1);
            }
        }
    }))
    .ToArray();


await Task.WhenAll(producerTasks.Concat(consumerTasks));
