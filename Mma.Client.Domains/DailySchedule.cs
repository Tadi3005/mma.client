namespace Mma.Client.Domains;

public record DailySchedule(IList<Slot> Slots)
{
    public IList<Slot> Slots { get; private set; } = Slots;
}
