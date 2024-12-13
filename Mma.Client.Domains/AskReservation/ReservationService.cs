namespace Mma.Client.Domains.AskReservation;

public class ReservationService(IList<IReservationValidator> validators, IList<User> users, Room room)
{
    public ReservationStatus Reserve(ReservationRequest reservation, IList<Reservation> events)
    {
        foreach (var t in validators)
        {
            var status = t.Validate(reservation, events, users, room);

            if (status != ReservationStatus.Accepted)
            {
                return status;
            }
        }

        return ReservationStatus.Accepted;
    }
}
