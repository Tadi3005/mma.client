namespace Mma.Client.Domains.AskReservation;

public class ReservationService
{
    private readonly IList<IReservationValidator> _validators;
    private readonly IList<User> _users;
    private readonly Room _room;

    public ReservationService(IList<IReservationValidator> validators, IList<User> users, Room room)
    {
        _validators = validators;
        _users = users;
        _room = room;
    }

    public ReservationStatus Reserve(ReservationRequest reservation, IList<Reservation> events) => _validators.Select(validator => validator.Validate(reservation, events, _users, _room)).FirstOrDefault(status => status != ReservationStatus.Accepted);
}
