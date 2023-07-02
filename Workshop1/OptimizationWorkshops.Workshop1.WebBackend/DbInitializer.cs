using System.Data;
using Microsoft.Data.Sqlite;

namespace OptimizationWorkshops.Workshop1.WebBackend;

public class DbInitializer
{
    private readonly SqliteConnection _dbConnection;

    public DbInitializer(SqliteConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task InitializeAsync()
    {
        var numberOfItems = 1000;

        if (_dbConnection.State is not ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        await using var checkCommand = _dbConnection.CreateCommand();
        checkCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='DataFrame';";
        var result = checkCommand.ExecuteScalar();
        
        if (result is not null)
        {
            return;
        }
        
        await using var createTableCommand = _dbConnection.CreateCommand();
        createTableCommand.CommandText = "CREATE TABLE IF NOT EXISTS DataFrame (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp INTEGER, X REAL, Y REAL, Z REAL);";
        createTableCommand.ExecuteNonQuery();

        while (numberOfItems > 0)
        {
            await using var insertCommand = _dbConnection.CreateCommand();
            insertCommand.CommandText = "INSERT INTO DataFrame (Timestamp, X, Y, Z) VALUES (@Timestamp, @X, @Y, @Z);";
        
            var timestampParameter = insertCommand.CreateParameter();
            timestampParameter.ParameterName = "@Timestamp";
            timestampParameter.Value = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        
            var xParameter = insertCommand.CreateParameter();
            xParameter.ParameterName = "@X";
            xParameter.Value = Random.Shared.NextDouble();
        
            var yParameter = insertCommand.CreateParameter();
            yParameter.ParameterName = "@Y";
            yParameter.Value = Random.Shared.NextDouble();
        
            var zParameter = insertCommand.CreateParameter();
            zParameter.ParameterName = "@Z";
            zParameter.Value = Random.Shared.NextDouble();
            
            insertCommand.Parameters.Add(timestampParameter);
            insertCommand.Parameters.Add(xParameter);
            insertCommand.Parameters.Add(yParameter);
            insertCommand.Parameters.Add(zParameter);

            insertCommand.ExecuteNonQuery();
            
            numberOfItems--;
        }
    }
}
