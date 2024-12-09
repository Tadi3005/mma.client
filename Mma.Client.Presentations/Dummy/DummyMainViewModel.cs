namespace Mma.Client.Presentations.Dummy;

public class DummyMainViewModel : IMainViewModel
{
    public IStateRoomViewModel StateRoomViewModel { get; set; } = new DummyStateRoomViewModel();
}
