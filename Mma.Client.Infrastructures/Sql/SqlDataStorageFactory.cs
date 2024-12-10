using Mma.Client.Domains.Data;
using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory : IDataStorageFactory
{
    private readonly MySqlConnection _connectionManager;

    public SqlDataStorageFactory(MySqlConnection connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public IDataStorage CreateDataStorage() => new SqlDataStorage(_connectionManager);
}
