using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlService(IDataStorage dataStorage) : IDataService
{
    public Room FindRoomById(string roomId) => dataStorage.RoomDao.FindById(roomId);

    public IList<Reservation> FindReservations(DateTime date, string roomId) => dataStorage.CalendarDao.FindByRoomIdAndDate(date, roomId);

    public void AddReservation(ReservationRequest request) => dataStorage.CalendarDao.Add(request);

    public IList<User> FindAllUsers() => dataStorage.UserDao.FindAll();
}
