namespace Mma.Client.Domains.Tests;

[TestFixture]
public class ScheduleServiceTests
{
    [Test]
    public void CreateSchedule_ShouldGenerateDailyScheduleWithSlots()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var slotGenerator = new SlotGenerator(openingHours);
        var scheduleService = new ScheduleService(slotGenerator);

        var date = new DateTime(2024, 12, 15);
        var reservations = new List<Reservation>();

        // Act
        var result = scheduleService.CreateSchedule(date, reservations);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Slots, Is.Not.Null);
    }

    [Test]
    public void IsWeekend_WhenDateIsSaturday_ShouldReturnTrue()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var slotGenerator = new SlotGenerator(openingHours);
        var scheduleService = new ScheduleService(slotGenerator);
        var saturday = new DateTime(2024, 12, 14);

        // Act
        var result = scheduleService.IsWeekend(saturday);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWeekend_WhenDateIsSunday_ShouldReturnTrue()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var slotGenerator = new SlotGenerator(openingHours);
        var scheduleService = new ScheduleService(slotGenerator);
        var sunday = new DateTime(2024, 12, 15);

        // Act
        var result = scheduleService.IsWeekend(sunday);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWeekend_WhenDateIsWeekday_ShouldReturnFalse()
    {
        // Arrange
        var openingHours = new OpeningHours(TimeSpan.FromHours(9), TimeSpan.FromHours(17));
        var slotGenerator = new SlotGenerator(openingHours);
        var scheduleService = new ScheduleService(slotGenerator);
        var weekday = new DateTime(2024, 12, 13); // Friday

        // Act
        var result = scheduleService.IsWeekend(weekday);

        // Assert
        Assert.That(result, Is.False);
    }
}
