namespace Mma.Client.Domains;

public record Room(string Id, string Name, int Capacity)
{
    public static readonly Room Empty = new (string.Empty, string.Empty, 0);

    public bool HasEnoughCapacity(int requestNumberOfPeople) => Capacity >= requestNumberOfPeople;

    public bool IsRoomAvailable(DateTime checkStart, DateTime checkEnd) => true;

    public string GetSummary() => $"{Name} - {Capacity}";
}
