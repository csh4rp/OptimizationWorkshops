using Microsoft.Data.Sqlite;
using WebBackendApp.Models;

namespace WebBackendApp.Services;

public class DataService : IDataService
{
    private readonly SqliteConnection _connection;

    public DataService(SqliteConnection connection) => _connection = connection;

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

        insertCommand.ExecuteNonQuery();
    }

    public async Task<DataFrameSummaryDto> GetSummaryAsync(CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        await using var command = _connection.CreateCommand();
        command.CommandText = "SELECT X, Y, Z FROM DataFrame;";

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

        var xDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(xData); 
        var yDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(yData);
        var zDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(zData);

        return new DataFrameSummaryDto
        {
            XStdDev = xDev,
            YStdDev = yDev,
            ZStdDev = zDev, 
            NumberOfDataFrames = xData.Count
        };
    }
}
