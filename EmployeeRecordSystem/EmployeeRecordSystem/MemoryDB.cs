using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace EmployeeRecordSystem
{
    internal class MemoryDB : IMemoryDB
    {
        private readonly string connectionString;

        public MemoryDB()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public void AddRecord(int key, string value)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("INSERT INTO Employees (EmployeeID, EmployeeName) VALUES (@key, @value)", connection);
            command.Parameters.AddWithValue("@key", key);
            command.Parameters.AddWithValue("@value", value);
            command.ExecuteNonQuery();
        }

        public void UpdateRecord(int key, string value)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("UPDATE Employees SET EmployeeName = @value WHERE EmployeeID = @key", connection);
            command.Parameters.AddWithValue("@key", key);
            command.Parameters.AddWithValue("@value", value);
            command.ExecuteNonQuery();
        }

        public void DeleteRecord(int key)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("DELETE FROM Employees WHERE EmployeeID = @key", connection);
            command.Parameters.AddWithValue("@key", key);
            command.ExecuteNonQuery();
        }

        public List<(int, string)> GetAllRecords()
        {
            var records = new List<(int, string)>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT EmployeeID, EmployeeName FROM Employees", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                records.Add((reader.GetInt32("EmployeeID"), reader.GetString("EmployeeName")));
            }
            return records;
        }

        public (int, string) FindRecord(int key)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT EmployeeID, EmployeeName FROM Employees WHERE EmployeeID = @key", connection);
            command.Parameters.AddWithValue("@key", key);
            using var reader = command.ExecuteReader();
            if (reader.Read())
                return (reader.GetInt32("EmployeeID"), reader.GetString("EmployeeName"));

            return (-1, null);
        }

        public List<(int Key, string Value)> Search(string stub)
        {
            var results = new List<(int, string)>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand("SELECT EmployeeID, EmployeeName FROM Employees WHERE EmployeeName LIKE @stub", connection);
            command.Parameters.AddWithValue("@stub", $"%{stub}%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add((reader.GetInt32("EmployeeID"), reader.GetString("EmployeeName")));
            }
            return results;
        }
    }
}