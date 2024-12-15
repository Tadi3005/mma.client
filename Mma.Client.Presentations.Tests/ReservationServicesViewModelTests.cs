using Mma.Client.Domains;

namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class ReservationServicesViewModelTests
    {
        private ReservationServicesViewModel _viewModel;
        private List<Service> _services;

        [SetUp]
        public void SetUp()
        {
            // Création de services de test
            _services =
            [
                new Service(1, "Service 1"),
                new Service(2, "Service 2")
            ];

            // Initialisation de ReservationServicesViewModel avec les services
            _viewModel = new ReservationServicesViewModel(_services);
        }

        [Test]
        public void Services_ShouldReturnCorrectNumberOfServices()
        {
            // Act
            var services = _viewModel.Services;

            // Assert
            Assert.That(services, Has.Count.EqualTo(2));
        }

        [Test]
        public void Services_ShouldReturnCorrectServiceNames()
        {
            // Act
            var services = _viewModel.Services;

            // Assert
            Assert.That(services.First().Name, Is.EqualTo("Service 1"));
            Assert.That(services.Last().Name, Is.EqualTo("Service 2"));
        }

        [Test]
        public void SelectedServices_ShouldReturnEmpty_WhenNoServicesAreSelected()
        {
            // Act
            var selectedServices = _viewModel.SelectedServices;

            // Assert
            Assert.That(selectedServices, Is.Empty);
        }

        [Test]
        public void SelectedServices_ShouldReturnSelectedServices_WhenSomeAreChecked()
        {
            // Arrange
            _viewModel.Services.ElementAt(0).IsChecked = true;

            // Act
            var selectedServices = _viewModel.SelectedServices;

            // Assert
            Assert.That(selectedServices, Has.Count.EqualTo(1));
            Assert.That(selectedServices.First().Name, Is.EqualTo("Service 1"));
        }

        [Test]
        public void SelectedServices_ShouldReturnCorrectSelectedServices_AfterMultipleSelections()
        {
            // Arrange
            _viewModel.Services.ElementAt(0).IsChecked = true;
            _viewModel.Services.ElementAt(1).IsChecked = true;

            // Act
            var selectedServices = _viewModel.SelectedServices;

            // Assert
            Assert.That(selectedServices, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(selectedServices.First().Name, Is.EqualTo("Service 1"));
                Assert.That(selectedServices.Last().Name, Is.EqualTo("Service 2"));
            });
        }

        [Test]
        public void SelectedServices_ShouldReflectServiceDeselect()
        {
            // Arrange
            _viewModel.Services.ElementAt(0).IsChecked = true;
            _viewModel.Services.ElementAt(1).IsChecked = true;
            _viewModel.Services.ElementAt(1).IsChecked = false;

            // Act
            var selectedServices = _viewModel.SelectedServices;

            // Assert
            Assert.That(selectedServices, Has.Count.EqualTo(1));
            Assert.That(selectedServices.First().Name, Is.EqualTo("Service 1"));
        }
    }
}
