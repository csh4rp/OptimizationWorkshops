using SearchingBenchmarkApp.Models;

namespace SearchingBenchmarkApp.Services;

public class DataService
{
    private readonly IEnumerable<DataFrame> _data;

    public DataService(IEnumerable<DataFrame> data) => _data = data;

    public double ProcessDataUsingDictionary(int[] ids)
    {
        var dictionary = _data.ToDictionary(k => k.Id);

        var sum = 0d;
        
        foreach (var id in ids)
        {
            if (dictionary.TryGetValue(id, out var item))
            {
                sum += item.X + item.Y + item.Z;
            }
        }

        return sum;
    }
    
    public double ProcessDataUsingList(int[] ids)
    {
        var list = _data.ToList();

        var sum = 0d;
        
        foreach (var id in ids)
        {
            var item = list.FirstOrDefault(f => f.Id == id);
            if (item is not null)
            {
                sum += item.X + item.Y + item.Z;
            }
        }

        return sum;
    }
}
