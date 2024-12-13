namespace Mma.Client.Domains.AskReservation.Validator;

public class RoomAvailabilityValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => reservations.Any(r => r.Overlaps(request))
            ? ReservationStatus.RoomNotAvailable
            : ReservationStatus.Accepted;
}
