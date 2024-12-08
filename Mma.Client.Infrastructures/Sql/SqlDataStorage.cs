using Mma.Client.Domains.Data;
using Mma.Client.Infrastructures.Sql.Dao;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorage : IDataStorage
{
    public IRoomDao RoomDao { get; } = new SqlRoomDao();
}
