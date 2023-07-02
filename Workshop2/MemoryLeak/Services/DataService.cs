using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using WebBackend.Models;
using WebBackend.Services;

namespace MemoryLeak.Services;

public class DataService : IDataService
{
    private readonly SqliteConnection _connection;
    private readonly IMemoryCache _memoryCache;

    public DataService(SqliteConnection connection, IMemoryCache memoryCache)
    {
        _connection = connection;
        _memoryCache = memoryCache;
    }

    public async Task SaveAsync(DataFrameDto dto, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        await using var insertCommand = _connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO DataFrame (Timestamp, X, Y, Z) VALUES (@Timestamp, @X, @Y, @Z);";

        var timestampParameter = insertCommand.CreateParameter();
        timestampParameter.ParameterName = "@Timestamp";
        timestampParameter.Value = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var xParameter = insertCommand.CreateParameter();
        xParameter.ParameterName = "@X";
        xParameter.Value = dto.X;

        var yParameter = insertCommand.CreateParameter();
        yParameter.ParameterName = "@Y";
        yParameter.Value = dto.Y;

        var zParameter = insertCommand.CreateParameter();
        zParameter.ParameterName = "@Z";
        zParameter.Value = dto.Z;

        insertCommand.Parameters.Add(timestampParameter);
        insertCommand.Parameters.Add(xParameter);
        insertCommand.Parameters.Add(yParameter);
        insertCommand.Parameters.Add(zParameter);

        insertCommand.ExecuteNonQuery();
    }

    public async Task<DataFrameSummaryDto> GetSummaryAsync(DateTimeOffset tillTime, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue<DataFrameSummaryDto>($"summary:{tillTime.ToUnixTimeMilliseconds()}", out var summary))
        {
            return summary!;
        }
        
        await _connection.OpenAsync(cancellationToken);

        await using var command = _connection.CreateCommand();
        command.CommandText = "SELECT X, Y, Z FROM DataFrame WHERE Timestamp <= @timestamp;";
        var timestampParameter = command.CreateParameter();
        timestampParameter.ParameterName = "@timestamp";
        timestampParameter.Value = tillTime.ToUnixTimeMilliseconds();
        command.Parameters.Add(timestampParameter);
        
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var xData = new List<double>();
        var yData = new List<double>();
        var zData = new List<double>();
        
        while (await reader.ReadAsync(cancellationToken))
        {
            var x = reader.GetDouble(0);
            var y = reader.GetDouble(1);
            var z = reader.GetDouble(2);
            
            xData.Add(x);
            yData.Add(y);
            zData.Add(z);
        }
        
        summary = new DataFrameSummaryDto
        {
            XStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(xData),
            YStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(yData), 
            ZStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(zData),
            X99Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(xData, 99),
            Y99Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(yData, 99),
            Z99Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(zData, 99),
            X95Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(xData, 95),
            Y95Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(yData, 95),
            Z95Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(zData, 95),
            X90Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(xData, 90),
            Y90Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(yData, 90),
            Z90Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(zData, 90),
            X75Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(xData, 75),
            Y75Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(yData, 75),
            Z75Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(zData, 75),
            X50Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(xData, 50),
            Y50Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(yData, 50),
            Z50Percentile = MathNet.Numerics.Statistics.Statistics.Percentile(zData, 50),
            NumberOfDataFrames = xData.Count
        };
        
        using var entry = _memoryCache.CreateEntry($"summary:{tillTime.ToUnixTimeMilliseconds()}");
        entry.Value = summary;

        return summary;
    }
}
