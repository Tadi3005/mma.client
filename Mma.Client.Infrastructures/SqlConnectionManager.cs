using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures;

public sealed class SqlConnectionManager(ConnectionStringBuilder connectionString) : IDisposable
{
    private readonly string _connectionString = $"Server={connectionString.DbServer};Port={connectionString.DbPort};Database={connectionString.DbDataBase};User Id={connectionString.DbUser};Password={connectionString.DbPassword}";

    private MySqlConnection? _connection;

    public MySqlConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
