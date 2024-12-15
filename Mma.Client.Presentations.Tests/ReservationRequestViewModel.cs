using NSubstitute;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class ReservationRequestViewModelTests
    {
        private ReservationRequestViewModel _viewModel;
        private IDateTimeReservationRequestViewModel _dateTimeReservationRequestViewModel;
        private IReservationServicesViewModel _reservationServicesViewModel;

        [SetUp]
        public void SetUp()
        {
            // Initialisation des mocks pour les dépendances
            _dateTimeReservationRequestViewModel = Substitute.For<IDateTimeReservationRequestViewModel>();
            _reservationServicesViewModel = Substitute.For<IReservationServicesViewModel>();

            // Initialisation de ReservationRequestViewModel
            _viewModel = new ReservationRequestViewModel(_dateTimeReservationRequestViewModel, _reservationServicesViewModel);
        }

        [Test]
        public void Matricule_ShouldInitializeAsEmpty()
        {
            // Act
            var matricule = _viewModel.Matricule;

            // Assert
            Assert.That(matricule, Is.Empty);
        }

        [Test]
        public void Description_ShouldInitializeAsEmpty()
        {
            // Act
            var description = _viewModel.Description;

            // Assert
            Assert.That(description, Is.Empty);
        }

        [Test]
        public void NumberOfPeople_ShouldInitializeAsOne()
        {
            // Act
            var numberOfPeople = _viewModel.NumberOfPeople;

            // Assert
            Assert.That(numberOfPeople, Is.EqualTo(1));
        }

        [Test]
        public void DateTimeReservationRequestViewModel_ShouldBeInjectedCorrectly()
        {
            // Act
            var dateTimeViewModel = _viewModel.DateTimeReservationRequestViewModel;

            // Assert
            Assert.That(dateTimeViewModel, Is.EqualTo(_dateTimeReservationRequestViewModel));
        }

        [Test]
        public void ReservationServicesViewModel_ShouldBeInjectedCorrectly()
        {
            // Act
            var servicesViewModel = _viewModel.ReservationServicesViewModel;

            // Assert
            Assert.That(servicesViewModel, Is.EqualTo(_reservationServicesViewModel));
        }

        [Test]
        public void SetMatricule_ShouldUpdateMatriculeProperty()
        {
            // Arrange
            var newMatricule = "123456";

            // Act
            _viewModel.Matricule = newMatricule;

            // Assert
            Assert.That(_viewModel.Matricule, Is.EqualTo(newMatricule));
        }

        [Test]
        public void SetDescription_ShouldUpdateDescriptionProperty()
        {
            // Arrange
            var newDescription = "Description de la réservation";

            // Act
            _viewModel.Description = newDescription;

            // Assert
            Assert.That(_viewModel.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void SetNumberOfPeople_ShouldUpdateNumberOfPeopleProperty()
        {
            // Arrange
            var newNumberOfPeople = 5;

            // Act
            _viewModel.NumberOfPeople = newNumberOfPeople;

            // Assert
            Assert.That(_viewModel.NumberOfPeople, Is.EqualTo(newNumberOfPeople));
        }
    }
}
