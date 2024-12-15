using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures;

public interface IDataStorageFactory
{
    IDataStorage CreateDataStorage();
}
