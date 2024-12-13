namespace Mma.Client.Domains.AskReservation.Validator;

public class UserIdValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => users.Any(u => u.Matricule == request.Matricule) ? ReservationStatus.Accepted : ReservationStatus.UserNotFound;
}
