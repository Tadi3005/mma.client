namespace Mma.Client.Domains.AskReservation;

public record ReservationRequest(string Matricule, DateTime Date, DateTime TimeStart, DateTime TimeEnd, int NumberOfPeople, string Description, IList<Service> Services)
{
    public bool IsEndBeforeStart => TimeEnd < TimeStart;

    public bool IsHoursExactly => (TimeStart.Minute != 0 && TimeStart.Minute != 30) || (TimeEnd.Minute != 0 && TimeEnd.Minute != 30);

    public bool IsPastDate => Date < DateTime.Now;
}
