namespace Mma.Client.Domains;

public class SlotGenerator(OpeningHours openingHours)
{
    public IList<Slot> Generate(DateTime date, IList<Reservation> reservations)
    {
        var slots = new List<Slot>();
        var startTime = date.Date.AddHours(openingHours.Start.Hours);
        var endTime = date.Date.AddHours(openingHours.End.Hours);

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
