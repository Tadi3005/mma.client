using Mma.Client.Domains.AskReservation;

namespace Mma.Client.Domains.Tests;

public class ReservationRequestTests
{
    [Test]
    public void IsEndBeforeStart_ShouldReturnTrue_WhenEndIsBeforeStart()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 11, 0, 0), // Start at 11:00 AM
            new DateTime(2024, 12, 18, 10, 30, 0), // End at 10:30 AM (before start)
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsEndBeforeStart;

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsEndBeforeStart_ShouldReturnFalse_WhenEndIsAfterStart()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 30, 0), // Start at 10:30 AM
            new DateTime(2024, 12, 18, 11, 0, 0),  // End at 11:00 AM (after start)
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsEndBeforeStart;

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHoursExactly_ShouldReturnTrue_WhenStartOrEndTimeIsNotOnTheHalfHour()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 15, 0), // Start at 10:15 AM (not on the half-hour)
            new DateTime(2024, 12, 18, 11, 15, 0), // End at 11:15 AM (not on the half-hour)
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsHoursExactly;

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHoursExactly_ShouldReturnFalse_WhenStartAndEndTimesAreOnTheHalfHour()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            new DateTime(2024, 12, 18),
            new DateTime(2024, 12, 18, 10, 0, 0), // Start at 10:00 AM (on the half-hour)
            new DateTime(2024, 12, 18, 10, 30, 0), // End at 10:30 AM (on the half-hour)
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsHoursExactly;

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsPast_ShouldReturnTrue_WhenRequestTimeIsInThePast()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            DateTime.Now.AddDays(-1), // Request date in the past
            DateTime.Now.AddHours(-2), // Start time in the past
            DateTime.Now.AddHours(-1), // End time in the past
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsPast;

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsPast_ShouldReturnFalse_WhenRequestTimeIsInTheFuture()
    {
        // Arrange
        var request = new ReservationRequest(
            "1234",
            DateTime.Now.AddDays(1), // Request date in the future
            DateTime.Now.AddHours(2), // Start time in the future
            DateTime.Now.AddHours(3), // End time in the future
            10,
            "Meeting",
            new List<Service>()
        );

        // Act
        var result = request.IsPast;

        // Assert
        Assert.That(result, Is.False);
    }
}
