namespace Mma.Client.Domains;

public class OpeningHours(TimeSpan start, TimeSpan end)
{
    public TimeSpan Start { get; } = start;

    public TimeSpan End { get; } = end;

    public bool IsOutside(DateTime requestTimeStart, DateTime requestTimeEnd)
    {
        var requestStart = requestTimeStart.TimeOfDay;
        var requestEnd = requestTimeEnd.TimeOfDay;

        return requestStart < Start || requestEnd > End;
    }
}
