using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Domains.Data;

namespace Mma.Client.Presentations;

public partial class DailyScheduleViewModel(IList<ISlotViewModel> slotViewModels, DailySchedule dailySchedule, Room room, IDataService service) : ObservableObject, IDailyScheduleViewModel
{
    [ObservableProperty]
    private string _selectedDate = DateTime.Now.ToString("yyyy-MM-dd");

    [ObservableProperty]
    private IReadOnlyCollection<ISlotViewModel> _slots = new ObservableCollection<ISlotViewModel>(slotViewModels);

    partial void OnSelectedDateChanged(string date)
    {
        DateTime selectedDate = DateTime.Parse(date);
        IList<Reservation> reservations = service.FindReservations(selectedDate, room.Id);

        dailySchedule.Slots = dailySchedule.GenerateSlots(selectedDate, reservations);

        var updatedSlotViewModels = dailySchedule.Slots.Select(slot =>
            new SlotViewModel(slot)).ToList();

        Slots = new ObservableCollection<ISlotViewModel>(updatedSlotViewModels);
    }
}
