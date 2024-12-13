using System.Data.Common;
using Mma.Client.Domains.Data.Dao;

namespace Mma.Client.Domains.Data;

public interface IDataStorage
{
    IRoomDao RoomDao { get; }

    ICalendarDao CalendarDao { get; }

    IUserDao UserDao { get; }

    public DbTransaction BeginTransaction();

    public void CommitTransaction(DbTransaction transaction);

    public void RollbackTransaction(DbTransaction transaction);
}
