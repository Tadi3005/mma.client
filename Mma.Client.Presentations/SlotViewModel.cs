using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a slot view model.
 * </summary>
 */
public partial class SlotViewModel(Slot slot) : ObservableObject, ISlotViewModel
{
    [ObservableProperty]
    private string _timeEnd = slot.End.ToString("HH:mm");

    [ObservableProperty]
    private string _hexColorBackground = slot.IsFree ? "#00ffa1" : "#ff01a0";
}
