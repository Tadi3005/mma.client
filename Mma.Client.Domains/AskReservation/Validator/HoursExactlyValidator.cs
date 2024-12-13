namespace Mma.Client.Domains.AskReservation.Validator;

public class HoursExactlyValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => request.IsHoursExactly ? ReservationStatus.HoursExactly : ReservationStatus.Accepted;
}
