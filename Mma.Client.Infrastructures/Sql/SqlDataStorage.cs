using System.Data;
using Mma.Client.Infrastructures.Mapper;
using Mma.Client.Infrastructures.Sql.Dao;
using Serilog;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorage(IDbConnection connection, ILogger logger) : IDataStorage
{
    public IRoomDao RoomDao => new SqlRoomDao(connection, new SqlRoomMapper(), logger);

    public ICalendarDao CalendarDao => new SqlCalendarDao(connection, new SqlCalendarMapper(), logger);

    public IUserDao UserDao => new SqlUserDao(connection, new SqlUserMapper(), logger);

    public IDbTransaction BeginTransaction() => connection.BeginTransaction();

    public void CommitTransaction(IDbTransaction transaction) => transaction.Commit();

    public void RollbackTransaction(IDbTransaction transaction) => transaction.Rollback();
}
