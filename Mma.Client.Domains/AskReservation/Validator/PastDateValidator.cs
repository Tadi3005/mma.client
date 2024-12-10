namespace Mma.Client.Domains.AskReservation.Validator;

public class PastDateValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
    {
        if (request.TimeStart < DateTime.Now || request.TimeEnd < DateTime.Now)
        {
            return ReservationStatus.PastDate;
        }

        return ReservationStatus.Accepted;
    }
}
