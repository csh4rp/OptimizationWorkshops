using System.Text;
using BenchmarkDotNet.Attributes;

namespace StringOperationsBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    [Params(1, 2, 5, 10, 100, 1000, 10000)]
    public int NumberOfItems { get; set; }
    
    [Benchmark(Description = "String concatenation")]
    public void RunStringConcat()
    {
        // ReSharper disable once NotAccessedVariable
        var str = "Test String";
        
        for (int i = 0; i < NumberOfItems; i++)
        {
            str += i;
        }
    }
    
    [Benchmark(Description = "String format")]
    public void RunStringFormat()
    {
        var str = "Test String";
        
        for (int i = 0; i < NumberOfItems; i++)
        {
            str = $"{str}{i}";
        }
    }
    
    [Benchmark(Description = "String builder")]
    public void RunStringBuilder()
    {
        var str = new StringBuilder("Test String");
        
        for (int i = 0; i < NumberOfItems; i++)
        {
            str.Append(i);
        }
    }
    
    [Benchmark(Description = "String builder with preset capacity")]
    public void RunStringBuilderWithCapacity()
    {
        var str = new StringBuilder("Test String", 10100);
        
        for (int i = 0; i < NumberOfItems; i++)
        {
            str.Append(i);
        }
    }
}
