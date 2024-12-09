using Mma.Client.Domains.Data.Dao;

namespace Mma.Client.Domains.Data;

public interface IDataStorage
{
    IRoomDao RoomDao { get; }

    ICalendarDao CalendarDao { get; }
}
