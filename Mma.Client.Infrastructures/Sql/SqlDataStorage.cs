using System.Data.Common;
using Mma.Client.Domains.Data;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Mapper;
using Mma.Client.Infrastructures.Sql.Dao;
using Serilog;

namespace Mma.Client.Infrastructures.Sql;

public class SqlDataStorage(DbConnection connection, ILogger logger) : IDataStorage
{
    public IRoomDao RoomDao => new SqlRoomDao(connection, new SqlRoomMapper(), logger);

    public ICalendarDao CalendarDao => new SqlCalendarDao(connection, new SqlCalendarMapper(), logger);

    public IUserDao UserDao => new SqlUserDao(connection, new SqlUserMapper(), logger);

    public DbTransaction BeginTransaction() => connection.BeginTransaction();

    public void CommitTransaction(DbTransaction transaction) => transaction.Commit();

    public void RollbackTransaction(DbTransaction transaction) => transaction.Rollback();
}
