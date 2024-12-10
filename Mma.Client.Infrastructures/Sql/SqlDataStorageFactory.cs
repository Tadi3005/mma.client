using System.Data.Common;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory(string connectionString, string providerName) : IDataStorageFactory
{
    private readonly DbProviderFactory _factory = DbProviderFactories.GetFactory(providerName);

    public IDataStorage CreateDataStorage()
    {
        try
        {
            var connection = _factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return new SqlDataStorage(connection);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Unable to establish a database connection.", e);
        }
    }
}
