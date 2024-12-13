using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a reservation view model.
 * </summary>
 */
public partial class ReservationViewModel(IReservationStatusViewModel statusViewModel, IReservationRequestViewModel reservationRequestViewModel, ReservationService reservationService,
    IDataService dataService, Room room) : ObservableObject, IReservationViewModel
{
    [ObservableProperty]
    private IReservationRequestViewModel _reservationRequestViewModel = reservationRequestViewModel;

    public ICommand AddReservation => new RelayCommand(OnAddReservation);

    [RelayCommand]
    private void OnAddReservation()
    {
        var date = DateTime.Parse(ReservationRequestViewModel.DateTimeReservationRequestViewModel.Date);
        var timeStart = DateTime.Parse(ReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeStart);
        var timeEnd = DateTime.Parse(ReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeEnd);

        // TODO: Récupérer les services sélectionnés par l'utilisateur
        var services = ReservationRequestViewModel.ReservationServicesViewModel.SelectedServices;
        var request = new ReservationRequest(ReservationRequestViewModel.Matricule, date, timeStart, timeEnd,
            ReservationRequestViewModel.NumberOfPeople, ReservationRequestViewModel.Description,
            services);
        var reservations = dataService.FindReservations(date, room.Id);
        var reservationStatus = reservationService.Reserve(request, reservations);

        ReservationStatusViewModel = new ReservationStatusViewModel(reservationStatus);

        if (reservationStatus == ReservationStatus.Accepted)
        {
            dataService.AddReservation(request, room.Id);
        }
    }

    [ObservableProperty]
    private IReservationStatusViewModel _reservationStatusViewModel = statusViewModel;
}
