using System.Data.Common;

namespace Mma.Client.Domains.Data;

public interface IDataStorageFactory
{
    IDataStorage CreateDataStorage(DbConnection connection);
}
