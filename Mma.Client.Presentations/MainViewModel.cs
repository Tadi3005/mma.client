using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public class MainViewModel(IStateRoomViewModel roomStateManagerViewModel, IDailyScheduleViewModel dailyScheduleViewModel,
    IReservationViewModel reservationViewModel) : ObservableObject, IMainViewModel
{
    public IDailyScheduleViewModel DailyScheduleViewModel { get; } = dailyScheduleViewModel;

    public IReservationViewModel ReservationViewModel { get; } = reservationViewModel;

    public IStateRoomViewModel StateRoomViewModel { get; set; } = roomStateManagerViewModel;
}
