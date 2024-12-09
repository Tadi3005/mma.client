using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;

namespace Mma.Client.Presentations;

public partial class SlotViewModel(Slot slot) : ObservableObject, ISlotViewModel
{
    [ObservableProperty]
    private string _timeStart = slot.Start.ToString("HH:mm");

    [ObservableProperty]
    private string _hexColorBackground = slot.IsFree ? "#00ffa1" : "#ff01a0";
}
