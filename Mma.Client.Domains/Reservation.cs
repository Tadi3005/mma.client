namespace Mma.Client.Domains;

public record Reservation(DateTime Date, DateTime Start, DateTime End, string Summary, string Description, Room Room, User User)
{
    public static Reservation Empty = new(DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, Room.Empty, User.Empty);
}
