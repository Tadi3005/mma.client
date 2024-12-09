namespace Mma.Client.Domains;

public record RoomState(Room room, IList<Reservation> reservations)
{
    public string IdRoom = room.Id;

    public string NameRoom = room.Name;

    public bool IsActualAvailable => !reservations.Any(r => r.Start <= DateTime.Now && r.End >= DateTime.Now);

    public DateTime TimeCurrentSlot = DateTime.Now.Minute < 30
        ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0)
        : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);

    public Reservation ActualReservation = reservations.FirstOrDefault(r => r.Start <= DateTime.Now && r.End >= DateTime.Now) ?? Reservation.Empty;

    public Reservation NextReservation = reservations.FirstOrDefault(r => r.Start > DateTime.Now) ?? Reservation.Empty;
}
