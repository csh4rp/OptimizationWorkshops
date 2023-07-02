var data = new List<byte[]>();

Console.WriteLine("\n" +
                  "Press 'r' to generate data,\n" +
                  "'0' to collect gen 0,\n" +
                  "'1' to collect gen 1,\n" +
                  "'2' to collect gen 2,\n" +
                  "'f' to collect gen 2 with forced compaction,\n" +
                  "'e' to exit");
    
var key = Console.ReadKey().KeyChar;
do
{
    switch (key)
    {
        case 'r':
        {
            data.Add(new byte[1000]);
            break;
        }
        case '0':
        {
            data.Clear();
            GC.Collect(0);
            var ephGen = GC.GetGCMemoryInfo(GCKind.Any);
            Console.WriteLine();
            Console.WriteLine("Gen 0 collection count: " + GC.CollectionCount(0));
            Console.WriteLine("Gen 0 size: " + ephGen.GenerationInfo[0].SizeBeforeBytes + " -> " +
                              ephGen.GenerationInfo[0].SizeAfterBytes);
            break;
        }
        case '1':
        {
            data.Clear();
            GC.Collect(1);
            var ephGen = GC.GetGCMemoryInfo(GCKind.Any);
            Console.WriteLine();
            Console.WriteLine("Gen 0 collection count: " + GC.CollectionCount(0));
            Console.WriteLine("Gen 1 collection count: " + GC.CollectionCount(1));
            Console.WriteLine("Gen 0 size: " + ephGen.GenerationInfo[0].SizeBeforeBytes + " -> " +
                              ephGen.GenerationInfo[0].SizeAfterBytes);
            Console.WriteLine("Gen 1 size: " + ephGen.GenerationInfo[1].SizeBeforeBytes + " -> " +
                              ephGen.GenerationInfo[1].SizeAfterBytes);
            break;
        }
        case '2':
        {
            data.Clear();
            GC.Collect(2);
            var gc = GC.GetGCMemoryInfo(GCKind.Any);
            Console.WriteLine();
            Console.WriteLine("Gen 0 collection count: " + GC.CollectionCount(0));
            Console.WriteLine("Gen 1 collection count: " + GC.CollectionCount(1));
            Console.WriteLine("Gen 2 collection count: " + GC.CollectionCount(2));
            Console.WriteLine("Gen 0 size: " + gc.GenerationInfo[0].SizeBeforeBytes + " -> " +
                              gc.GenerationInfo[0].SizeAfterBytes);
            Console.WriteLine("Gen 1 size: " + gc.GenerationInfo[1].SizeBeforeBytes + " -> " +
                              gc.GenerationInfo[1].SizeAfterBytes);
            Console.WriteLine("Gen 2 size: " + gc.GenerationInfo[2].SizeBeforeBytes + " -> " +
                              gc.GenerationInfo[2].SizeAfterBytes);
            break;
        }
        case 'f':
            data.Clear();
            GC.Collect(2, GCCollectionMode.Aggressive, true, true);
            break;
    }

    Console.WriteLine();
    key = Console.ReadKey().KeyChar;
}
while(key != 'e');
