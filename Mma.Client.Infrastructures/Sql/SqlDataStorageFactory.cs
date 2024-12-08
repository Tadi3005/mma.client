using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory : IDataStorageFactory
{
    public IDataStorage CreateDataStorage() => new SqlDataStorage();
}
