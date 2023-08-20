using BenchmarkDotNet.Attributes;

namespace SpansBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private const string ValueToFind = "T00020";
    
    private const string Value = "00001,00002,00003,00004,00005,00006,00007,00008,00009,00010"
                                 + "T00011,T00012,T00013,T00014,T00015,T00016,T00017,T00018,T00019,T00020";
    
    [Benchmark]
    public void RunSpan()
    {
        var startIndex = 0;
        var endIndex = Value.IndexOf(',');

        var valueToFind = ValueToFind.AsSpan();
        var value = Value.AsSpan();

        while (endIndex != -1)
        {
            var currentValue = value.Slice(startIndex, endIndex - startIndex);

            if (valueToFind.SequenceEqual(currentValue))
            {
                break;
            }

            startIndex = endIndex + 1;
            endIndex = Value.IndexOf(',', startIndex);
            if (endIndex == -1 && startIndex < Value.Length)
            {
                endIndex = Value.Length;
            }
        }
    }
    
    [Benchmark]
    public void RunSubstring()
    {
        var startIndex = 0;
        var endIndex = Value.IndexOf(',');

        string value;
        
        while (endIndex != -1)
        {
            value = Value.Substring(startIndex, endIndex - startIndex);

            if (value == ValueToFind)
            {
                break;
            }

            startIndex = endIndex + 1;
            endIndex = Value.IndexOf(',', startIndex);
            if (endIndex == -1 && startIndex < Value.Length)
            {
                endIndex = Value.Length;
            }
        }
    }
    
    [Benchmark]
    public void RunStringSplit()
    {
        var items = Value.Split(',');
        
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] == ValueToFind)
            {
                break;
            }
        }
    }
}
