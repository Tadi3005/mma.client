using Mma.Client.Domains;

public class DailySchedule
{
    public IList<Slot> Slots { get; set; } = new List<Slot>();

    public DailySchedule(DateTime date, IList<Reservation> reservations)
    {
        Slots = GenerateSlots(date, reservations);
    }

    public IList<Slot> GenerateSlots(DateTime date, IList<Reservation> reservations)
    {
        var slots = new List<Slot>();
        var startTime = date.Date.AddHours(8);
        var endTime = date.Date.AddHours(17);

        while (startTime < endTime)
        {
            var slotEnd = startTime.AddMinutes(30);

            var isFree = !reservations.Any(r => r.Start < slotEnd && r.End > startTime);
            slots.Add(new Slot(startTime, slotEnd, isFree));
            startTime = slotEnd;
        }

        return slots;
    }
}
