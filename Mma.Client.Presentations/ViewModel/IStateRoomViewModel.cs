namespace Mma.Client.Presentations.ViewModel;

public interface IStateRoomViewModel
{
    public string ColorStateRoom { get; set; }

    public string RoomName { get; set; }

    public string TimeCurentSlot { get; set; }

    public IActualStateRoomViewModel ActualStateRoomViewModel { get; set; }

    public string TimeNextReservation { get; set; }
}
