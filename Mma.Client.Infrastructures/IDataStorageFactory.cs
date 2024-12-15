namespace Mma.Client.Infrastructures;

public interface IDataStorageFactory
{
    IDataStorage CreateDataStorage();
}
