namespace Mma.Client.Domains;

public record Slot(DateTime Start, DateTime End, bool IsFree)
{
}
