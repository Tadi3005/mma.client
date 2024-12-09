using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory : IDataStorageFactory
{
    private readonly SqlConnectionManager _connectionManager;

    public SqlDataStorageFactory(SqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public IDataStorage CreateDataStorage() => new SqlDataStorage(_connectionManager.Connection);
}
