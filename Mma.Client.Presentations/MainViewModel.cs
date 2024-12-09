using Mma.Client.Domains.Data;

namespace Mma.Client.Presentations;

public class MainViewModel : IMainViewModel
{
    public MainViewModel(IStateRoomViewModel stateRoomViewModel)
    {
        IStateRoomViewModel = stateRoomViewModel;
    }

    public IStateRoomViewModel IStateRoomViewModel { get; }
}
