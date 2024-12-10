namespace Mma.Client.Domains;

public record RoomState(Room Room, IList<Reservation> Reservations)
{
    public string IdRoom => Room.Id;

    public string NameRoom => Room.Name;

    public bool IsActualAvailable => !Reservations.Any(r => r.Start <= DateTime.Now && r.End >= DateTime.Now);

    public DateTime TimeCurrentSlot => DateTime.Now.Minute < 30
        ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0)
        : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);

    public Reservation ActualReservation => Reservations.FirstOrDefault(r => r.Start <= DateTime.Now && r.End >= DateTime.Now) ?? Reservation.Empty;

    public Reservation NextReservation => Reservations.FirstOrDefault(r => r.Start > DateTime.Now) ?? Reservation.Empty;
}
