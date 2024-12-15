using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Presentations.Tests;

public class ReservationStatusViewModelTest
{
    [TestFixture]
    public class ReservationStatusViewModelTests
    {
        [TestCase(ReservationStatus.Accepted, "Réservation acceptée", "#00FF00")]
        [TestCase(ReservationStatus.UserNotFound, "Matricule inconnu", "#FF0000")]
        [TestCase(ReservationStatus.RoomCapacityExceeded, "Capacité insuffisante", "#FF0000")]
        [TestCase(ReservationStatus.RoomNotAvailable, "Créneau indisponible", "#FF0000")]
        [TestCase(ReservationStatus.EndBeforeStart, "La fin est avant le début", "#FF0000")]
        [TestCase(ReservationStatus.OutsideOpeningHours, "En dehors des heures d'ouverture", "#FF0000")]
        [TestCase(ReservationStatus.PastDate, "Date ou heure passée", "#FF0000")]
        [TestCase(ReservationStatus.HoursExactly, "Les heures ne sont pas exactes (créneaux de 30 minutes)", "#FF0000")]
        [TestCase(ReservationStatus.DescriptionTooShort, "Description trop courte (5 caractères minimum)", "#FF0000")]
        [TestCase(ReservationStatus.None, "Remplissez votre réservation", "#FFFFFF")]
        public void ReservationStatusViewModel_ShouldSetStatusAndStatusColorCorrectly(ReservationStatus status, string expectedStatus, string expectedStatusColor)
        {
            // Arrange
            var viewModel = new ReservationStatusViewModel(status);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(viewModel.Status, Is.EqualTo(expectedStatus));
                Assert.That(viewModel.StatusColorHex, Is.EqualTo(expectedStatusColor));
            });
        }
    }
}
