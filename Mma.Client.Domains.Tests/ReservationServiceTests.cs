using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.AskReservation.Validator;

namespace Mma.Client.Domains.Tests;

public class ReservationServiceTests
{
    private ReservationService _reservationService;

    [SetUp]
    public void SetUp()
    {
        IList<User> users = new List<User>() { new("123456", "John Doe", "john.doe@gmail.com") };
        Room room = new("L1", "Room 1", 10);
        IList<IReservationValidator> validators = new List<IReservationValidator>
        {
            new UserIdValidator(),
            new RoomCapacityValidator(),
            new RoomAvailabilityValidator(),
            new EndBeforeStartValidator(),
            new OutsideOpeningHoursValidator(new OpeningHours(new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0))),
            new PastDateValidator(),
            new HoursExactlyValidator(),
        };
        _reservationService = new ReservationService(validators, users, room);
    }

    [Test]
    public void Reserve_ShouldReturnAccepted_WhenReservationIsValid()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 0, 0),
            new DateTime(2024, 12, 18, 11, 0, 0),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.Accepted));
    }

    [Test]
    public void Reserve_ShouldReturnRejected_WhenUserNotFound()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "NOT_FOUND",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 0, 0),
            new DateTime(2024, 12, 18, 11, 0, 0),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.UserNotFound));
    }

    [Test]
    public void Reserve_ShouldReturnRejected_WhenRoomCapacityExceeded()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 0, 0),
            new DateTime(2024, 12, 18, 11, 0, 0),
            11,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.RoomCapacityExceeded));
    }

    [Test]
    public void Reserve_ShouldReturnRejected_WhenRoomNotAvailable()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            DateTime.Now,
            DateTime.Now.AddHours(1),
            DateTime.Now.AddHours(2),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>
        {
            new(DateTime.Now, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2), "Summary", "description", new Room("L1", "Room", 10), new User("123456", "John Doe", "john.doe@gmail.com")) };

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.RoomNotAvailable));
    }

    [Test]
    public void EndBeforeStartValidator_ShouldReject_WhenEndTimeIsBeforeStartTime()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 11, 0, 0),
            new DateTime(2024, 12, 18, 10, 30, 0),  // End time before start time
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.EndBeforeStart));
    }

    [Test]
    public void OutsideOpeningHoursValidator_ShouldReject_WhenReservationIsOutsideOpeningHours()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 7, 0, 0),  // Before 8:00 AM
            new DateTime(2024, 12, 18, 8, 0, 0),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.OutsideOpeningHours));
    }

    [Test]
    public void PastDateValidator_ShouldReject_WhenReservationIsInThePast()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            DateTime.Today.AddDays(-1),  // Request date in the past
            DateTime.Today.AddHours(8),
            DateTime.Today.AddHours(10),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.PastDate));
    }

    [Test]
    public void HoursExactlyValidator_ShouldReject_WhenStartAndEndTimesAreNotOnTheHour()
    {
        // Arrange
        var reservation = new ReservationRequest(
            "123456",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 15, 0),  // Start time not on the hour
            new DateTime(2024, 12, 18, 11, 0, 0),
            10,
            "Meeting",
            new List<Service>()
        );
        var events = new List<Reservation>();

        // Act
        var result = _reservationService.Reserve(reservation, events);

        // Assert
        Assert.That(result, Is.EqualTo(ReservationStatus.HoursExactly));
    }
}
