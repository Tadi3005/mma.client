namespace Mma.Client.Domains.AskReservation;

public interface IReservationValidator
{
    ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room);
}
