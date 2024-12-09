namespace Mma.Client.Presentations;

public interface IStateRoomViewModel
{
    public string ColorStateRoom { get; set; }

    public string RoomName { get; set; }

    public string TimeCurentSlot { get; set; }

    public string NameActualReservation { get; set; }

    public string TimeActualReservation { get; set; }

    public string TimeNextReservation { get; set; }
}
