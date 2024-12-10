namespace Mma.Client.Domains.AskReservation.Validator;

public class HoursExactlyValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
    {
        if ((request.TimeStart.Minute != 0 && request.TimeStart.Minute != 30) || (request.TimeEnd.Minute != 0 && request.TimeEnd.Minute != 30))
        {
            return ReservationStatus.HoursExactly;
        }

        return ReservationStatus.Accepted;
    }
}
