using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public partial class ReservationViewModel : ObservableObject, IReservationViewModel
{
    private readonly ReservationService _reservationService;

    private readonly IDataService _dataService;

    private readonly Room _room;

    public ReservationViewModel(IReservationStatusViewModel statusViewModelViewModel, ReservationService reservationService,
        IDataService dataService, Room room)
    {
        ReservationStatusViewModel = statusViewModelViewModel;
        _reservationService = reservationService;
        _dataService = dataService;
        _room = room;
        AddReservation = new RelayCommand(OnAddReservation);
    }

    [ObservableProperty]
    private string _matricule = string.Empty;

    [ObservableProperty]
    private string _date = DateTime.Now.ToString("yyyy-MM-dd");

    [ObservableProperty]
    private string _timeStart = DateTime.Now.ToString("HH:mm");

    [ObservableProperty]
    private string _timeEnd = DateTime.Now.AddHours(1).ToString("HH:mm");

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private int _numberOfPeople = 1;

    public IRelayCommand AddReservation { get; }

    private void OnAddReservation()
    {
        var date = DateTime.Parse(Date);
        var timeStart = DateTime.Parse(TimeStart);
        var timeEnd = DateTime.Parse(TimeEnd);
        var request = new ReservationRequest(Matricule, date, timeStart, timeEnd, NumberOfPeople, Description);
        var reservations = _dataService.FindReservations(date, _room.Id);
        var reservationStatus = _reservationService.Reserve(request, reservations);

        ReservationStatusViewModel = new ReservationStatusViewModel(reservationStatus);

        if (reservationStatus == ReservationStatus.Accepted)
        {
            _dataService.AddReservation(request);
        }
    }

    [ObservableProperty]
    private IReservationStatusViewModel _reservationStatusViewModel = new ReservationStatusViewModel(ReservationStatus.None);
}
