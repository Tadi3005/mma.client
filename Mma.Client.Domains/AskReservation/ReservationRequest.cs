namespace Mma.Client.Domains.AskReservation;

public record ReservationRequest(string Matricule, DateTime RequestDate, DateTime TimeStart, DateTime TimeEnd, int NumberOfPeople, string Description, IList<Service> Services)
{
    public bool IsEndBeforeStart => TimeEnd < TimeStart;

    public bool IsHoursExactly => (TimeStart.Minute != 0 && TimeStart.Minute != 30) || (TimeEnd.Minute != 0 && TimeEnd.Minute != 30);

    public bool IsPast
    {
        get
        {
            var requestDateStart = RequestDate.Date + TimeStart.TimeOfDay;
            var requestDateEnd = RequestDate.Date + TimeEnd.TimeOfDay;
            return requestDateStart < DateTime.Now || requestDateEnd < DateTime.Now;
        }
    }
}
