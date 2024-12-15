using System.Data;
using System.Data.Common;

namespace Mma.Client.Infrastructures;

public interface IDataStorage
{
    IRoomDao RoomDao { get; }

    ICalendarDao CalendarDao { get; }

    IUserDao UserDao { get; }

    public IDbTransaction BeginTransaction();

    public void CommitTransaction(IDbTransaction transaction);

    public void RollbackTransaction(IDbTransaction transaction);
}
