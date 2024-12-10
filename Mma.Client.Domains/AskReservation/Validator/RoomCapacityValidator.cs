namespace Mma.Client.Domains.AskReservation.Validator;

public class RoomCapacityValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room) => room.Capacity < request.NumberOfPeople ? ReservationStatus.RoomCapacityExceeded : ReservationStatus.Accepted;
}
