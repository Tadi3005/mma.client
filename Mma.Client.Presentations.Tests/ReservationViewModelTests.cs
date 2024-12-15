using NSubstitute;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class ReservationViewModelTests
    {
        private IReservationRequestViewModel _mockReservationRequestViewModel;
        private IReservationStatusViewModel _mockReservationStatusViewModel;
        private IReservationService _mockReservationService;
        private IDataService _mockDataService;
        private Room _mockRoom;

        [SetUp]
        public void SetUp()
        {
            _mockRoom = Substitute.For<Room>("L1", "Room 1", 10);
            _mockReservationRequestViewModel = Substitute.For<IReservationRequestViewModel>();
            _mockReservationStatusViewModel = Substitute.For<IReservationStatusViewModel>();
            _mockReservationService = Substitute.For<IReservationService>();
            _mockDataService = Substitute.For<IDataService>();
        }

        [Test]
        public void AddReservation_WhenReservationStatusIsAccepted_ShouldAddReservation()
        {
            // Arrange
            var viewModel = new ReservationViewModel(_mockReservationStatusViewModel, _mockReservationRequestViewModel, _mockReservationService, _mockDataService, _mockRoom);
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.Date.Returns("2022-01-01");
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeStart.Returns("08:00");
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeEnd.Returns("10:00");
            _mockReservationRequestViewModel.Matricule.Returns("123456");
            _mockReservationRequestViewModel.NumberOfPeople.Returns(5);
            _mockReservationRequestViewModel.Description.Returns("Description");
            _mockReservationRequestViewModel.ReservationServicesViewModel.SelectedServices.Returns(new List<Service>() { new(1, "Servic 1") });
            _mockDataService.FindReservations(Arg.Any<DateTime>(), Arg.Any<string>()).Returns(new List<Reservation>());
            _mockReservationService.Reserve(Arg.Any<ReservationRequest>(), Arg.Any<List<Reservation>>()).Returns(ReservationStatus.Accepted);

            // Act
            viewModel.AddReservation.Execute(null);

            // Assert
            _mockDataService.Received(1).AddReservation(Arg.Any<ReservationRequest>(), Arg.Any<string>());
        }

        [Test]
        public void AddReservation_WhenReservationStatusIsRejected_ShouldNotAddReservation()
        {
            // Arrange
            var viewModel = new ReservationViewModel(_mockReservationStatusViewModel, _mockReservationRequestViewModel, _mockReservationService, _mockDataService, _mockRoom);
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.Date.Returns("2022-01-01");
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeStart.Returns("08:00");
            _mockReservationRequestViewModel.DateTimeReservationRequestViewModel.TimeEnd.Returns("10:00");
            _mockReservationRequestViewModel.Matricule.Returns("123456");
            _mockReservationRequestViewModel.NumberOfPeople.Returns(5);
            _mockReservationRequestViewModel.Description.Returns("Description");
            _mockReservationRequestViewModel.ReservationServicesViewModel.SelectedServices.Returns(new List<Service>() { new(1, "Servic 1") });
            _mockDataService.FindReservations(Arg.Any<DateTime>(), Arg.Any<string>()).Returns(new List<Reservation>());
            _mockReservationService.Reserve(Arg.Any<ReservationRequest>(), Arg.Any<List<Reservation>>()).Returns(ReservationStatus.UserNotFound);

            // Act
            viewModel.AddReservation.Execute(null);

            // Assert
            _mockDataService.DidNotReceive().AddReservation(Arg.Any<ReservationRequest>(), Arg.Any<string>());
        }
    }
}
