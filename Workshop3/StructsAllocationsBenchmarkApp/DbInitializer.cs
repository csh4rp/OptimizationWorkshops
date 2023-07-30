using Microsoft.Data.Sqlite;

namespace StructsAllocationsBenchmarkApp;

public class DbInitializer : IDisposable
{
    private readonly SqliteConnection _dbConnection;

    public DbInitializer() => _dbConnection = new SqliteConnection("Data Source=mydb.db;");

    public async Task InitializeAsync()
    {
        await _dbConnection.OpenAsync();

        await using var createTableCommand = _dbConnection.CreateCommand();
        createTableCommand.CommandText = "CREATE TABLE IF NOT EXISTS DataFrame (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp INTEGER, X REAL, Y REAL, Z REAL);";
        createTableCommand.ExecuteNonQuery();

        for (int i = 0; i < 1000; i++)
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
        }
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}
