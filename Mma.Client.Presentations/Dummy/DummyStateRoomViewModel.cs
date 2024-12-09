namespace Mma.Client.Presentations.Dummy;

public class DummyStateRoomViewModel : IStateRoomViewModel
{
    public string ColorStateRoom { get; set; } = "#FF0000";

    public string RoomName { get; set; } = "LC1 - Learning Center 1";

    public string TimeCurentSlot { get; set; } = "lundi 21 octobre, créneau de 10h30";
    public string NameActualReservation { get; set; } = "Réunion de travail";
    public string TimeActualReservation { get; set; } = "lundi 21 octobre, créneau de 10h30";
    public string TimeNextReservation { get; set; } = "lundi 21 octobre, créneau de 11h30";
}
