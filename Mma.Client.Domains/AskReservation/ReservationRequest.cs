namespace Mma.Client.Domains.AskReservation;

public record ReservationRequest(string Matricule, DateTime Date, DateTime TimeStart, DateTime TimeEnd, int NumberOfPeople, string Description)
{
}
