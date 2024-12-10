using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public partial class DailyScheduleViewModel(IList<ISlotViewModel> slotViewModels, DailySchedule dailySchedule, Room room, IDataService service) : ObservableObject, IDailyScheduleViewModel
{
    [ObservableProperty]
    private string _selectedDate = DateTime.Now.ToString("yyyy-MM-dd");

    [ObservableProperty]
    private IReadOnlyCollection<ISlotViewModel> _slots = new ObservableCollection<ISlotViewModel>(slotViewModels);

    partial void OnSelectedDateChanged(string value)
    {
        DateTime selectedDate = DateTime.Parse(value);
        IList<Reservation> reservations = service.FindReservations(selectedDate, room.Id);

        dailySchedule.Slots = dailySchedule.GenerateSlots(selectedDate, reservations);

        var updatedSlotViewModels = dailySchedule.Slots.Select(slot =>
            new SlotViewModel(slot)).ToList();

        Slots = new ObservableCollection<ISlotViewModel>(updatedSlotViewModels);
    }
}
