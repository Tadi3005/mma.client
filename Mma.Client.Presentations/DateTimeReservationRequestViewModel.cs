using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a date time reservation request view model.
 * </summary>
 */
public partial class DateTimeReservationRequestViewModel : ObservableObject, IDateTimeReservationRequestViewModel
{
    [ObservableProperty]
    private string _date = DateTime.Now.ToString("yyyy-MM-dd");

    [ObservableProperty]
    private string _timeStart = DateTime.Now.ToString("HH:mm");

    [ObservableProperty]
    private string _timeEnd = DateTime.Now.AddHours(1).ToString("HH:mm");
}
