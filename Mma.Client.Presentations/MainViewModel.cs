using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;

namespace Mma.Client.Presentations;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    public MainViewModel(IStateRoomViewModel stateRoomViewModel)
    {
        _stateRoomViewModel = stateRoomViewModel;
    }

    [ObservableProperty]
    private IStateRoomViewModel _stateRoomViewModel;

    public void Refresh(RoomState roomState) => StateRoomViewModel = new StateRoomViewModel(roomState);
}
