namespace Mma.Client.Domains.AskReservation.Validator;

public class OutsideOpeningHoursValidator(OpeningHours openingHours) : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => openingHours.IsOutside(request.TimeStart, request.TimeEnd) ? ReservationStatus.OutsideOpeningHours : ReservationStatus.Accepted;
}
