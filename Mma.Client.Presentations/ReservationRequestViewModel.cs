using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a reservation request view model.
 * </summary>
 */
public partial class ReservationRequestViewModel(IDateTimeReservationRequestViewModel dateTimeReservationRequestViewModel,
    IReservationServicesViewModel reservationServicesViewModel)
    : ObservableObject, IReservationRequestViewModel
{
    [ObservableProperty]
    private string _matricule = string.Empty;

    [ObservableProperty]
    private IDateTimeReservationRequestViewModel _dateTimeReservationRequestViewModel = dateTimeReservationRequestViewModel;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private int _numberOfPeople = 1;

    public IReservationServicesViewModel ReservationServicesViewModel => reservationServicesViewModel;
}
