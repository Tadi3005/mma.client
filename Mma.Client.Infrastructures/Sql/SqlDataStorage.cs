using Mma.Client.Domains.Data;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Mapper;
using Mma.Client.Infrastructures.Sql.Dao;
using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorage(MySqlConnection connection) : IDataStorage
{
    public IRoomDao RoomDao { get; } = new SqlRoomDao(connection, new SqlRoomMapper());

    public ICalendarDao CalendarDao { get; } = new SqlCalendarDao(connection, new SqlCalendarMapper());

    public IUserDao UserDao { get; } = new SqlUserDao(connection, new SqlUserMapper());
}
