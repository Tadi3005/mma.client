namespace Mma.Client.Domains;

public class DailySchedule
{
    public DailySchedule()
    {
        Slots = GenerateSlots();
    }

    private IList<Slot> GenerateSlots()
    {
        var slots = new List<Slot>();
        var startTime = DateTime.Now.Date.AddHours(8);
        var endTime = DateTime.Now.Date.AddHours(17);

        while (startTime < endTime)
        {
            var slotEnd = startTime.AddMinutes(30);
            slots.Add(new Slot(startTime, slotEnd, true));
            startTime = slotEnd;
        }

        return slots;
    }
    public IList<Slot> Slots { get; set; } = new List<Slot>();
}
