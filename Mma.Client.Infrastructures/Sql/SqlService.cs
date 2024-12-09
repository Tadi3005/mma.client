using Mma.Client.Domains;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlService(IDataStorage dataStorage) : IDataService
{
    public Room FindRoomById(string roomId) => dataStorage.RoomDao.FindById(roomId);

    public IList<Reservation> FindReservations(DateTime date, string roomId) => dataStorage.CalendarDao.FindByRoomIdAndDate(date, roomId);
}
