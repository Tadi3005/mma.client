using System.Collections.ObjectModel;

namespace Mma.Client.Presentations;

public interface IDailyScheduleViewModel
{
    public string SelectedDate { get; set; }

    public IReadOnlyCollection<ISlotViewModel> Slots { get; set; }
}
