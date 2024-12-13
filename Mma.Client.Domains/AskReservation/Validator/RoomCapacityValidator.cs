namespace Mma.Client.Domains.AskReservation.Validator;

public class RoomCapacityValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => room.HasEnoughCapacity(request.NumberOfPeople) ? ReservationStatus.Accepted : ReservationStatus.RoomCapacityExceeded;
}
