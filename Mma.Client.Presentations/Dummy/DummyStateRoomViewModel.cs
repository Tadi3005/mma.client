namespace Mma.Client.Presentations.Dummy;

public class DummyStateRoomViewModel : IStateRoomViewModel
{
    public string ColorStateRoom { get; set; } = "#FF0000";
    public string RoomName { get; set; } = "Room 1";
    public string TimeCurentSlot { get; set; } = "12:00";
    public string NameActualSlot { get; set; } = "Slot A";
    public string TimeActualEvent { get; set; } = "12:30";
    public string TimeNextEvent { get; set; } = "13:00";
}
