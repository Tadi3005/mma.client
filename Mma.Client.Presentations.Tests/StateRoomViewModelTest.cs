using Mma.Client.Domains;

namespace Mma.Client.Presentations.Tests;

public class StateRoomViewModelTest
{
    [TestFixture]
    public class ActualStateRoomViewModelTests
    {
        private RoomState _state;


        [SetUp]
        public void SetUp()
        {
            Room room = new("Room123", "Test Room", 10);
            IList<Reservation> reservations = new List<Reservation>
            {
                new(DateTime.Now, DateTime.Now.AddHours(-1),DateTime.Now.AddHours(1), "Summary", "Description", room, User.Empty),
                new(DateTime.Now.Date, DateTime.Now.AddHours(3),DateTime.Now.AddHours(4), "Summary", "Description", room, User.Empty)
            };

            _state = new RoomState(room, reservations);
        }


        [Test]
        public void ActualStateRoomViewModel_ShouldReturnCorrectReservationDetails_WhenNoReservationOverlapsWithCurrentTime()
        {
            // Arrange
            var viewModel = new StateRoomViewModel(new ActualStateRoomViewModel(_state), _state);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(viewModel.ActualStateRoomViewModel.TimeActualReservation, Is.EqualTo($"de {DateTime.Now.AddHours(-1):HH:mm} à {DateTime.Now.AddHours(1):HH:mm}"));
                Assert.That(viewModel.ActualStateRoomViewModel.NameActualReservation, Is.EqualTo("Summary"));
                Assert.That(viewModel.ColorStateRoom, Is.EqualTo("#FF0000"));
                Assert.That(viewModel.RoomName, Is.EqualTo("Room123 - Test Room"));
            });
        }

        [Test]
        public void ActualStateRoomViewModel_ShouldReturnCorrectReservationDetails_WhenReservationOverlapsWithCurrentTime()
        {
            // Arrange
            var viewModel = new StateRoomViewModel(new ActualStateRoomViewModel(_state), _state);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(viewModel.TimeNextReservation, Is.EqualTo($"de {DateTime.Now.AddHours(3):HH:mm} à {DateTime.Now.AddHours(4):HH:mm}"));
            });
        }
    }
}
