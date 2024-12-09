using CommandLine;

namespace Mma.Client.App;

public class Options
{
    [Option('d', "db", Required = true, HelpText = "Database connection string")]
    public string ConnectionString { get; set; } = string.Empty;

    [Option('r', "room", Required = true, HelpText = "Room id")]
    public string RoomId { get; set; } = string.Empty;
}
