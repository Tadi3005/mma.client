namespace Mma.Client.Presentations.Dummy;

public class DummyMainViewModel : IMainViewModel
{
    public IStateRoomViewModel IStateRoomViewModel { get; } = new DummyStateRoomViewModel();
}
