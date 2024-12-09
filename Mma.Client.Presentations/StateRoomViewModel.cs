using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;

namespace Mma.Client.Presentations
{
    public partial class StateRoomViewModel(RoomState roomState) : ObservableObject, IStateRoomViewModel
    {
        [ObservableProperty]
        private string _colorStateRoom = roomState.IsActualAvailable ? "#00FF00" : "#FF0000";

        [ObservableProperty]
        private string _roomName = $"{roomState.IdRoom} - {roomState.NameRoom}";

        [ObservableProperty]
        private string _timeCurentSlot = $"{roomState.TimeCurrentSlot.ToString("dddd dd MMMM")} - créneau de {roomState.TimeCurrentSlot.ToString("HH:mm")}";

        [ObservableProperty]
        private string _nameActualReservation = roomState.ActualReservation == Reservation.Empty ? "Aucun événement en cours" : roomState.ActualReservation.Summary;

        [ObservableProperty]
        public string _timeActualReservation = roomState.ActualReservation == Reservation.Empty ? string.Empty
            : $"de roomState.ActualReservation.Start.ToString(HH:mm) à roomState.ActualReservation.End.ToString(HH:mm)";

        [ObservableProperty]
        public string _timeNextReservation = roomState.NextReservation == Reservation.Empty ? "Disponnible jusqu'à la fin de la journée" :
            $"de {roomState.NextReservation.Start.ToString("HH:mm")} à {roomState.NextReservation.End.ToString("HH:mm")}";
    }
}
