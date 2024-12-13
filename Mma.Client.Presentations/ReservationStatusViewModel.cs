using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a reservation status view model.
 * </summary>
 */
public partial class ReservationStatusViewModel(ReservationStatus status) : ObservableObject, IReservationStatusViewModel
{
    [ObservableProperty]
    private string _status = status switch
    {
        ReservationStatus.Accepted => "Réservation acceptée",
        ReservationStatus.UserNotFound => "Matricule inconnu",
        ReservationStatus.RoomCapacityExceeded => "Capacité insuffisante",
        ReservationStatus.RoomNotAvailable => "Salle non disponible",
        ReservationStatus.EndBeforeStart => "La fin est avant le début",
        ReservationStatus.OutsideOpeningHours => "En dehors des heures d'ouverture",
        ReservationStatus.PastDate => "Date passée",
        ReservationStatus.HoursExactly => "Les heures ne sont pas exactes (créneaux de 30 minutes)",
        ReservationStatus.DescriptionTooShort => "Description trop courte (5 caractères minimum)",
        ReservationStatus.None => "Remplissez votre réservation",
        _ => "Réservation refusée"
    };

    [ObservableProperty]
    private string _statusColorHex = status switch
    {
        ReservationStatus.Accepted => "#00FF00",
        ReservationStatus.None => "#FFFFFF",
        _ => "#FF0000"
    };
}
