using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public partial class StateRoomViewModel(RoomState roomState) : ObservableObject, IStateRoomViewModel
{
    [ObservableProperty]
    private string _colorStateRoom = roomState.IsActualAvailable ? "#00FF00" : "#FF0000";

    [ObservableProperty]
    private string _roomName = $"{roomState.IdRoom} - {roomState.NameRoom}";

    [ObservableProperty]
    private string _timeCurentSlot = $"{roomState.TimeCurrentSlot:dddd dd MMMM} - créneau de {roomState.TimeCurrentSlot:HH:mm}";

    [ObservableProperty]
    private string _nameActualReservation = roomState.ActualReservation == Reservation.Empty ? "Aucun événement en cours" : roomState.ActualReservation.Summary;

    [ObservableProperty]
    private string _timeActualReservation = roomState.ActualReservation == Reservation.Empty ? string.Empty
        : $"de {roomState.ActualReservation.Start:HH:mm} à {roomState.ActualReservation.End:HH:mm}";

    [ObservableProperty]
    private string _timeNextReservation = roomState.NextReservation == Reservation.Empty ? "Disponnible jusqu'à la fin de la journée" :
        $"de {roomState.NextReservation.Start:HH:mm} à {roomState.NextReservation.End:HH:mm}";
}
