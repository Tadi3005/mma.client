namespace Mma.Client.Domains.Data.Dao;

public interface ICalendarDao
{
    IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId);
}
