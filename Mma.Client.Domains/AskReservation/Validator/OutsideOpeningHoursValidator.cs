namespace Mma.Client.Domains.AskReservation.Validator;

public class OutsideOpeningHoursValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
    {
        if (request.TimeStart.Hour < 8 || request.TimeEnd.Hour > 17)
        {
            return ReservationStatus.OutsideOpeningHours;
        }

        return ReservationStatus.Accepted;
    }
}
