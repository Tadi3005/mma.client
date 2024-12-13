using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents the main view model.
 * </summary>
 */
public partial class MainViewModel(IStateRoomViewModel roomStateManagerViewModel, IDailyScheduleViewModel dailyScheduleViewModel,
    IReservationViewModel reservationViewModel) : ObservableObject, IMainViewModel
{
    public IDailyScheduleViewModel DailyScheduleViewModel { get; } = dailyScheduleViewModel;

    public IReservationViewModel ReservationViewModel { get; } = reservationViewModel;

    [ObservableProperty]
    private IStateRoomViewModel _stateRoomViewModel = roomStateManagerViewModel;
}
