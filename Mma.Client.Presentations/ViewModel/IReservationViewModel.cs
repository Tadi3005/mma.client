using System.Windows.Input;

namespace Mma.Client.Presentations.ViewModel;

public interface IReservationViewModel
{
    public IReservationRequestViewModel ReservationRequestViewModel { get; set; }

    public IReservationStatusViewModel ReservationStatusViewModel { get; set; }

    public ICommand AddReservation { get; }

    public string RoomReservation { get; }
}
