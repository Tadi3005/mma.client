namespace Mma.Client.Domains.AskReservation.Validator;

public class EndBeforeStartValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => request.IsEndBeforeStart ? ReservationStatus.EndBeforeStart : ReservationStatus.Accepted;
}
