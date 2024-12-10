namespace Mma.Client.Domains.AskReservation.Validator;

public class RoomAvailabilityValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room) => reservations.Any(e => e.Start <= request.TimeEnd && e.End >= request.TimeStart) ? ReservationStatus.RoomNotAvailable : ReservationStatus.Accepted;
}
