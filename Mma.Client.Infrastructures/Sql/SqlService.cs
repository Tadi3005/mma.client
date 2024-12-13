using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public class SqlService(IDataStorage dataStorage) : IDataService
{
    public Room FindRoomById(string roomId) => dataStorage.RoomDao.FindById(roomId);

    public IList<Reservation> FindReservations(DateTime date, string roomId) => dataStorage.CalendarDao.FindByRoomIdAndDate(date, roomId);

    public void AddReservation(ReservationRequest request, string roomId)
    {
        using var transaction = dataStorage.BeginTransaction();
        try
        {
            dataStorage.CalendarDao.Add(request, roomId);
            var idReservation = dataStorage.CalendarDao.GetLastInsertedId();
            foreach (var service in request.Services)
            {
                dataStorage.CalendarDao.Add(service.Id.ToString(), idReservation);
            }

            dataStorage.CommitTransaction(transaction);
        }
        catch (Exception)
        {
            dataStorage.RollbackTransaction(transaction);
            throw new InvalidOperationException("Error while adding reservation");
        }
    }

    public IList<User> FindAllUsers() => dataStorage.UserDao.FindAll();

    public IList<Service> FindAllServices() => dataStorage.CalendarDao.FindAll();
}
