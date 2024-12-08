using Mma.Client.Domains.Data;

namespace Mma.Client.Presentations;

public class MainViewModel : IMainViewModel
{
    public MainViewModel(IStateRoomViewModel stateRoomViewModel, IDataService dataService)
    {
        IStateRoomViewModel = stateRoomViewModel;
    }

    public IStateRoomViewModel IStateRoomViewModel { get; }
}
