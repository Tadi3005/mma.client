namespace Mma.Client.Domains.AskReservation;

public interface IReservationService
{
    ReservationStatus Reserve(ReservationRequest reservation, IList<Reservation> events);
}
