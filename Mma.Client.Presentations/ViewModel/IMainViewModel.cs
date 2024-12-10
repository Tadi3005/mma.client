namespace Mma.Client.Presentations.ViewModel;

public interface IMainViewModel
{
    IStateRoomViewModel StateRoomViewModel { get; }

    IDailyScheduleViewModel DailyScheduleViewModel { get; }

    IReservationViewModel ReservationViewModel { get; }
}
