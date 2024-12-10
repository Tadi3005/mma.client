using CommunityToolkit.Mvvm.Input;

namespace Mma.Client.Presentations.ViewModel;

public interface IReservationViewModel
{
    public string Matricule { get; set; }

    public string Date { get; set; }

    public string TimeStart { get; set; }

    public string TimeEnd { get; set; }

    public string Description { get; set; }

    public int NumberOfPeople { get; set; }

    public IRelayCommand AddReservation { get; }

    public IReservationStatusViewModel ReservationStatusViewModel { get; set; }

}
