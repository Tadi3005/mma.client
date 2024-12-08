using Mma.Client.Domains;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlService(IDataStorage dataStorage) : IDataService
{
    public Room FindRoomById(string roomId) => new("1", "Room 1", 10);
}
