namespace Mma.Client.Domains.Tests;

[TestFixture]
public class RoomTest
{
    [Test]
    public void EmptyRoom_ShouldHaveDefaultValues()
    {
        // Arrange
        var emptyRoom = Room.Empty;

        Assert.Multiple(() =>
        {
            // Act & Assert
            Assert.That(emptyRoom.Id, Is.EqualTo(string.Empty));
            Assert.That(emptyRoom.Name, Is.EqualTo(string.Empty));
            Assert.That(emptyRoom.Capacity, Is.EqualTo(0));
        });
    }

    [Test]
    public void HasEnoughCapacity_WhenCapacityIsSufficient_ShouldReturnTrue()
    {
        // Arrange
        var room = new Room("R1", "Room 1", 10);

        // Act
        var result = room.HasEnoughCapacity(8);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void HasEnoughCapacity_WhenCapacityIsInsufficient_ShouldReturnFalse()
    {
        // Arrange
        var room = new Room("R1", "Room 1", 10);

        // Act
        var result = room.HasEnoughCapacity(12);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRoomAvailable_ShouldAlwaysReturnTrue()
    {
        // Arrange
        var room = new Room("R1", "Room 1", 10);
        var start = DateTime.Now;
        var end = start.AddHours(2);

        // Act
        var result = room.IsRoomAvailable(start, end);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GetSummary_ShouldReturnCorrectSummary()
    {
        // Arrange
        var room = new Room("R1", "Room 1", 10);

        // Act
        var summary = room.GetSummary();

        // Assert
        Assert.That(summary, Is.EqualTo("Room 1 - 10"));
    }

    [Test]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        const string id = "R1";
        const string name = "Room 1";
        const int capacity = 10;

        // Act
        var room = new Room(id, name, capacity);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(room.Id, Is.EqualTo(id));
            Assert.That(room.Name, Is.EqualTo(name));
            Assert.That(room.Capacity, Is.EqualTo(capacity));
        });
    }
}
