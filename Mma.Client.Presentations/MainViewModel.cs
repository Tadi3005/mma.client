using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    public MainViewModel(IStateRoomViewModel stateRoomViewModel, IDailyScheduleViewModel dailyScheduleViewModel, IReservationViewModel reservationViewModel)
    {
        StateRoomViewModel = stateRoomViewModel;
        DailyScheduleViewModel = dailyScheduleViewModel;
        ReservationViewModel = reservationViewModel;
    }

    [ObservableProperty]
    private IStateRoomViewModel _stateRoomViewModel;

    public IDailyScheduleViewModel DailyScheduleViewModel { get; }

    public IReservationViewModel ReservationViewModel { get; }

    public void Refresh(IStateRoomViewModel roomState) => StateRoomViewModel = roomState;
}
