using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Domains.Data.Dao;

public interface ICalendarDao
{
    IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId);

    void Add(ReservationRequest request, string idRoom);

    void Add(string idService, string idReservation);

    IList<Service> FindAll();

    string GetLastInsertedId();
}
