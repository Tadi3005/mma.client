namespace Mma.Client.Domains;

public record ScheduleService(SlotGenerator SlotGenerator)
{
    public DailySchedule CreateSchedule(DateTime date, IList<Reservation> reservations)
    {
        var slots = SlotGenerator.Generate(date, reservations);
        return new DailySchedule(slots);
    }

    public bool IsWeekend(DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}
