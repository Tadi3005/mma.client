using Avalonia.Controls;
using Serilog;

namespace Mma.Client.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public ILogger? Logger { get; init; }

    public void DoSomething()
    {
        Logger?.Information("Hello de la vue");
    }
}
