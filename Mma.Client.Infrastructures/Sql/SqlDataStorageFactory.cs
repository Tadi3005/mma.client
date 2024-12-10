using System.Data.Common;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory : IDataStorageFactory
{
    private readonly string _connectionString;
    private readonly DbProviderFactory _factory;

    public SqlDataStorageFactory(string connectionString, string providerName)
    {
        _connectionString = connectionString;
        _factory = DbProviderFactories.GetFactory(providerName);
    }

    public IDataStorage CreateDataStorage()
    {
        try
        {
            var connection = _factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return new SqlDataStorage(connection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to establish a database connection.", e);
        }
    }
}
