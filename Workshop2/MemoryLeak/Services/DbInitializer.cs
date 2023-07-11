using System.Data;
using Microsoft.Data.Sqlite;

namespace MemoryLeak.Services;

public class DbInitializer : IDisposable
{
    private readonly SqliteConnection _dbConnection;

    public DbInitializer(IConfiguration configuration) => _dbConnection = new SqliteConnection(configuration.GetConnectionString("Sqlite"));

    public async Task InitializeAsync()
    {
        await _dbConnection.OpenAsync();

        await using var createTableCommand = _dbConnection.CreateCommand();
        createTableCommand.CommandText = "CREATE TABLE IF NOT EXISTS DataFrame (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp INTEGER, X REAL, Y REAL, Z REAL);";
        createTableCommand.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}
