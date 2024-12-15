namespace Mma.Client.Domains.Tests;

public class OpeningHoursTest
{
    [Test]
    public void IsOutside_ShouldReturnFalse_WhenRequestIsWithinHours()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 10, 0, 0); // 10:00 AM
        var requestEnd = new DateTime(2024, 12, 18, 16, 0, 0);   // 4:00 PM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOutside_ShouldReturnTrue_WhenRequestStartsBeforeOpening()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 8, 0, 0); // 8:00 AM
        var requestEnd = new DateTime(2024, 12, 18, 10, 0, 0);  // 10:00 AM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOutside_ShouldReturnTrue_WhenRequestEndsAfterClosing()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 16, 0, 0); // 4:00 PM
        var requestEnd = new DateTime(2024, 12, 18, 18, 0, 0);   // 6:00 PM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOutside_ShouldReturnTrue_WhenRequestIsCompletelyOutsideHours()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 7, 0, 0); // 7:00 AM
        var requestEnd = new DateTime(2024, 12, 18, 8, 0, 0);  // 8:00 AM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOutside_ShouldReturnFalse_WhenRequestExactlyMatchesOpeningHours()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 9, 0, 0); // 9:00 AM
        var requestEnd = new DateTime(2024, 12, 18, 17, 0, 0);  // 5:00 PM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOutside_ShouldReturnTrue_WhenRequestOverlapsOpeningHours()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var requestStart = new DateTime(2024, 12, 18, 8, 30, 0); // 8:30 AM
        var requestEnd = new DateTime(2024, 12, 18, 9, 30, 0);   // 9:30 AM

        // Act
        var result = openingHours.IsOutside(requestStart, requestEnd);

        // Assert
        Assert.That(result, Is.True);
    }
}
