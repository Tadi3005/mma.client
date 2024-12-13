using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a daily schedule view model.
 * </summary>
 */
public partial class DailyScheduleViewModel(IList<ISlotViewModel> slotViewModels, ScheduleService dailyScheduleService,
    Room room, IDataService dataService) : ObservableObject, IDailyScheduleViewModel
{
    [ObservableProperty]
    private string _selectedDate = DateTime.Now.ToString("yyyy-MM-dd");

    [ObservableProperty]
    private IReadOnlyCollection<ISlotViewModel> _slots = new ObservableCollection<ISlotViewModel>(slotViewModels);

    partial void OnSelectedDateChanged(string value)
    {
        var date = DateTime.Parse(value);
        var reservations = dataService.FindReservations(date, room.Id);

        var dailySchedule = dailyScheduleService.CreateSchedule(date, reservations);

        var updatedSlotViewModels = dailySchedule.Slots.Select(slot =>
            new SlotViewModel(slot)).ToList();

        Slots = new ObservableCollection<ISlotViewModel>(updatedSlotViewModels);
    }
}
