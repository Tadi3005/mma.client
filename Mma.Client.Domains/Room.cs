namespace Mma.Client.Domains;

public record Room(string Id, string Name, int Capacity)
{
    public static readonly Room Empty = new Room(string.Empty, string.Empty, 0);
}
