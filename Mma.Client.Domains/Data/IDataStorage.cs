namespace Mma.Client.Domains.Data;

public interface IDataStorage
{
    IRoomDao RoomDao { get; }
}
