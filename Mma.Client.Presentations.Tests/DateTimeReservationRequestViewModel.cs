namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class DateTimeReservationRequestViewModelTests
    {
        private DateTimeReservationRequestViewModel _viewModel;

        [SetUp]
        public void SetUp() =>
            // Initialisation de l'objet DateTimeReservationRequestViewModel
            _viewModel = new DateTimeReservationRequestViewModel();

        [Test]
        public void Date_ShouldInitializeWithCurrentDateAndTime()
        {
            // Arrange
            var expectedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            // Act
            var actualDate = _viewModel.Date;

            // Assert
            Assert.That(actualDate, Is.EqualTo(expectedDate));
        }

        [Test]
        public void TimeStart_ShouldInitializeWithCurrentTime()
        {
            // Arrange
            var expectedTimeStart = DateTime.Now.ToString("HH:mm");

            // Act
            var actualTimeStart = _viewModel.TimeStart;

            // Assert
            Assert.That(actualTimeStart, Is.EqualTo(expectedTimeStart));
        }

        [Test]
        public void TimeEnd_ShouldInitializeWithOneHourAfterCurrentTime()
        {
            // Arrange
            var expectedTimeEnd = DateTime.Now.AddHours(1).ToString("HH:mm");

            // Act
            var actualTimeEnd = _viewModel.TimeEnd;

            // Assert
            Assert.That(actualTimeEnd, Is.EqualTo(expectedTimeEnd));
        }

        [Test]
        public void SetDate_ShouldUpdateDateProperty()
        {
            // Arrange
            var newDate = "2024-12-20 10:00";

            // Act
            _viewModel.Date = newDate;

            // Assert
            Assert.That(_viewModel.Date, Is.EqualTo(newDate));
        }

        [Test]
        public void SetTimeStart_ShouldUpdateTimeStartProperty()
        {
            // Arrange
            var newTimeStart = "12:00";

            // Act
            _viewModel.TimeStart = newTimeStart;

            // Assert
            Assert.That(_viewModel.TimeStart, Is.EqualTo(newTimeStart));
        }

        [Test]
        public void SetTimeEnd_ShouldUpdateTimeEndProperty()
        {
            // Arrange
            var newTimeEnd = "14:00";

            // Act
            _viewModel.TimeEnd = newTimeEnd;

            // Assert
            Assert.That(_viewModel.TimeEnd, Is.EqualTo(newTimeEnd));
        }
    }
}
