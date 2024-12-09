using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;

namespace Mma.Client.Presentations;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    public MainViewModel(IStateRoomViewModel stateRoomViewModel, IDailyScheduleViewModel dailyScheduleViewModel)
    {
        StateRoomViewModel = stateRoomViewModel;
        DailyScheduleViewModel = dailyScheduleViewModel;
    }

    [ObservableProperty]
    private IStateRoomViewModel _stateRoomViewModel;

    public void Refresh(RoomState roomState) => StateRoomViewModel = new StateRoomViewModel(roomState);

    public IDailyScheduleViewModel DailyScheduleViewModel { get; set; }
}
