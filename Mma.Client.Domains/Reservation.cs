using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Domains;

public record Reservation(DateTime Date, DateTime Start, DateTime End, string Summary, string Description, Room Room, User User)
{
    public static readonly Reservation Empty = new(DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, Room.Empty, User.Empty);

    public bool Overlaps(ReservationRequest request)
    {
        if (Date.Date != request.Date.Date)
        {
            return false;
        }

        return
            Start.TimeOfDay < request.TimeEnd.TimeOfDay &&
            End.TimeOfDay > request.TimeStart.TimeOfDay;
    }

    public bool IsRoomAvailable(DateTime checkStart, DateTime checkEnd) => !(checkEnd <= Start || checkStart >= End);

    public string GetSummary() => $"{Summary} - {Date.ToShortDateString()}";

}
