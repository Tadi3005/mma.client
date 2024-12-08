namespace Mma.Client.Presentations;

public interface IStateRoomViewModel
{
    public string ColorStateRoom { get; set; }

    public string RoomName { get; set; }

    public string TimeCurentSlot { get; set; }

    public string NameActualSlot { get; set; }

    public string TimeActualEvent { get; set; }

    public string TimeNextEvent { get; set; }
}
