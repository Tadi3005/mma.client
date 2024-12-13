namespace Mma.Client.Presentations.ViewModel;

public interface IReservationRequestViewModel
{
    public string Matricule { get; set; }

    public IDateTimeReservationRequestViewModel DateTimeReservationRequestViewModel { get; set; }

    public string Description { get; set; }

    public int NumberOfPeople { get; set; }

    public IReservationServicesViewModel ReservationServicesViewModel { get; }
}
