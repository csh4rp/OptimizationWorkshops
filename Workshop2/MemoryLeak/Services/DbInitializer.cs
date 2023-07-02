using System.Data;
using Microsoft.Data.Sqlite;

namespace MemoryLeak.Services;

public class DbInitializer
{
    private readonly SqliteConnection _dbConnection;

    public DbInitializer(SqliteConnection dbConnection) => _dbConnection = dbConnection;

    public async Task InitializeAsync()
    {
        if (_dbConnection.State is not ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        await using var createTableCommand = _dbConnection.CreateCommand();
        createTableCommand.CommandText = "CREATE TABLE IF NOT EXISTS DataFrame (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp INTEGER, X REAL, Y REAL, Z REAL);";
        createTableCommand.ExecuteNonQuery();
    }
}
