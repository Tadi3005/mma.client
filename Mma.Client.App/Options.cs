using CommandLine;

namespace Mma.Client.App;

public class Options
{
    //TODO: déclarer des arguments reçus au lancement
    [Option('a', "arg", Required = true, HelpText = "Un argument quelconque.")]
    public string Arg { get; set; } = string.Empty;
}
