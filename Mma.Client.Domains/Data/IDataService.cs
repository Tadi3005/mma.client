using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Domains.Data;

public interface IDataService
{
    Room FindRoomById(string roomId);

    IList<Reservation> FindReservations(DateTime date, string roomId);

    void AddReservation(ReservationRequest request, string roomId);
}
