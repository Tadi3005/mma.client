using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a state room view model.
 * </summary>
 */
public partial class StateRoomViewModel(IActualStateRoomViewModel actualStateRoomViewModel, RoomState roomState) : ObservableObject, IStateRoomViewModel
{
    [ObservableProperty]
    private string _colorStateRoom = roomState.IsActualAvailable ? "#00FF00" : "#FF0000";

    [ObservableProperty]
    private string _roomName = $"{roomState.IdRoom} - {roomState.NameRoom}";

    [ObservableProperty]
    private string _timeCurentSlot = $"{roomState.TimeCurrentSlot:dddd dd MMMM} - créneau de {roomState.TimeCurrentSlot:HH:mm}";

    [ObservableProperty]
    private IActualStateRoomViewModel _actualStateRoomViewModel = actualStateRoomViewModel;

    [ObservableProperty]
    private string _timeNextReservation = roomState.NextReservation == Reservation.Empty ? "Disponnible jusqu'à la fin de la journée" :
        $"de {roomState.NextReservation.Start:HH:mm} à {roomState.NextReservation.End:HH:mm}";
}
