using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * ViewModel for the actual state of a room.
 * </summary>
 */
public partial class ActualStateRoomViewModel(RoomState roomState) : ObservableObject, IActualStateRoomViewModel
{
    [ObservableProperty]
    private string _nameActualReservation = roomState.ActualReservation == Reservation.Empty ? "Aucun événement en cours" : roomState.ActualReservation.Summary;

    [ObservableProperty]
    private string _timeActualReservation = roomState.ActualReservation == Reservation.Empty ? string.Empty
        : $"de {roomState.ActualReservation.Start:HH:mm} à {roomState.ActualReservation.End:HH:mm}";
}
