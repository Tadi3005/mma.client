using System.Data.Common;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorageFactory : IDataStorageFactory
{
    public IDataStorage CreateDataStorage(DbConnection connection) => new SqlDataStorage(connection);
}
