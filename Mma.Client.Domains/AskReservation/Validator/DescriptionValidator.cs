namespace Mma.Client.Domains.AskReservation.Validator;

public class DescriptionValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
    {
        if (request.Description is { Length: < 5 })
        {
            return ReservationStatus.DescriptionTooShort;
        }

        return ReservationStatus.Accepted;
    }
}
