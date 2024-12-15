using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Infrastructures;

public interface ICalendarDao
{
    IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId);

    void Add(ReservationRequest request, string idRoom);

    string GetLastInsertedId();

    void Add(string idService, string idReservation);

    IList<Service> FindAll();
}
