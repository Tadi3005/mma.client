namespace Mma.Client.Presentations.ViewModel;

public interface IDailyScheduleViewModel
{
    public string SelectedDate { get; set; }

    public IReadOnlyCollection<ISlotViewModel> Slots { get; set; }

    public string MessageError { get; set; }
}
