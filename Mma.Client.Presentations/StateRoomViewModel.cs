using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;

namespace Mma.Client.Presentations
{
    public partial class StateRoomViewModel(Room room) : ObservableObject, IStateRoomViewModel
    {
        [ObservableProperty]
        private string _colorStateRoom = "Green";

        [ObservableProperty]
        private string _roomName = $"{room.Id} - {room.Name}";

        [ObservableProperty]
        private string _timeCurentSlot = "12:00";

        [ObservableProperty]
        private string _nameActualSlot = "Slot A";

        [ObservableProperty]
        private string _timeActualEvent = "12:30";

        [ObservableProperty]
        private string _timeNextEvent = "13:00";
    }
}
