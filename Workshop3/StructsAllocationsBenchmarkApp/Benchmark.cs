using System.Buffers;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.Sqlite;
using StructsAllocationsBenchmarkApp.Models;

namespace StructsAllocationsBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private readonly SqliteConnection _connection;

    public Benchmark()
    {
        _connection = new("Data Source=mydb.db;");
        _connection.Open();
    }
    
    [Benchmark]
    public void RunClass()
    {
        var data = new List<DataFrameClass>();
        
        using var createTableCommand = _connection.CreateCommand();
        createTableCommand.CommandText = "SELECT Timestamp, X, Y, Z FROM DataFrame LIMIT 1000;";
        using var reader = createTableCommand.ExecuteReader();

        while (reader.Read())
        {
            var timestamp = reader.GetInt64(0);
            var x = reader.GetDouble(1);
            var y = reader.GetDouble(2);
            var z = reader.GetDouble(3);
            
            data.Add(new DataFrameClass
            {
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp),
                X = x,
                Y = y,
                Z = z
            });
        }
    }
    
    [Benchmark]
    public void RunStruct()
    {
        var data = new List<DataFrameStruct>();
        
        using var createTableCommand = _connection.CreateCommand();
        createTableCommand.CommandText = "SELECT Timestamp, X, Y, Z FROM DataFrame LIMIT 1000;";
        using var reader = createTableCommand.ExecuteReader();

        while (reader.Read())
        {
            var timestamp = reader.GetInt64(0);
            var x = reader.GetDouble(1);
            var y = reader.GetDouble(2);
            var z = reader.GetDouble(3);
            
            data.Add(new DataFrameStruct
            {
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp),
                X = x,
                Y = y,
                Z = z
            });
        }
    }
    
    [Benchmark]
    public void RunClassPreAllocated()
    {
        var data = new List<DataFrameClass>(1000);

        using var createTableCommand = _connection.CreateCommand();
        createTableCommand.CommandText = "SELECT Timestamp, X, Y, Z FROM DataFrame LIMIT 1000;";
        using var reader = createTableCommand.ExecuteReader();
        
        while (reader.Read())
        {
            var timestamp = reader.GetInt64(0);
            var x = reader.GetDouble(1);
            var y = reader.GetDouble(2);
            var z = reader.GetDouble(3);
            
            data.Add(new DataFrameClass
            {
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp),
                X = x,
                Y = y,
                Z = z
            });
        }
    }
    
    [Benchmark]
    public void RunStructBuffer()
    {
        var data = new List<DataFrameStruct>(1000);

        using var createTableCommand = _connection.CreateCommand();
        createTableCommand.CommandText = "SELECT Timestamp, X, Y, Z FROM DataFrame LIMIT 1000;";
        using var reader = createTableCommand.ExecuteReader();
        
        while (reader.Read())
        {
            var timestamp = reader.GetInt64(0);
            var x = reader.GetDouble(1);
            var y = reader.GetDouble(2);
            var z = reader.GetDouble(3);
            
            data.Add(new DataFrameStruct
            {
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp),
                X = x,
                Y = y,
                Z = z
            });
        }
    }
}
