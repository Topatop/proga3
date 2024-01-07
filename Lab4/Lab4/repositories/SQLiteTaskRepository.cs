using System.Data.SQLite;
using Lab4.exceptions;
using Newtonsoft.Json;

namespace Lab4.repositories;

public class SQLiteTaskRepository<T>
{
    private string _connectionString;
    
    public void Save(T data, string databasePath)
    {
        CreateDatabaseIfNotExists(databasePath);
        
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                // сначала почистим таблицу (вдруг там что-то есть)
                // тк мы будем хранить сериализованный объект в БД
                command.CommandText = "DELETE FROM Tasks";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Data (SerializedData) VALUES (@SerializedData)";
                command.Parameters.AddWithValue("@SerializedData", JsonConvert.SerializeObject(data));
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Data saved to SQLite successfully.");
    }

    public T Load(string databasePath)
    {
        if (File.Exists(databasePath))
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT SerializedData from Data ORDER BY ID DESC";
            var result = command.ExecuteScalar().ToString();
            
            if (result != null)
                return JsonConvert.DeserializeObject<T>(result);
            
            throw new TaskException("Unable to load :(");
        }
        throw new ArgumentException("Incorrect database path.");
    }

    private void CreateDatabaseIfNotExists(string databasePath)
    {
        _connectionString = $"Data Source={databasePath};Version=3;";
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS Data (ID INTEGER PRIMARY KEY, SerializedData TEXT)";
        command.ExecuteNonQuery();
    }
}