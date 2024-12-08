using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommandLine;
using Mma.Client.Infrastructures.Sql;
using Mma.Client.Presentations;
using Mma.Client.Views;
using Serilog;

namespace Mma.Client.App;

public partial class App : Application
{
    private static readonly ILogger Logger = new LoggerConfiguration()
        .WriteTo
        .Console()
        .WriteTo.File("log.txt")
        .CreateLogger();
    private MainWindow? _mainWindow;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Parser.Default.ParseArguments<Options>(desktop.Args)
                .WithNotParsed(errors =>
                {
                    Logger.Error("Errors in command-line args detected");
                    desktop.MainWindow = new Window();
                    desktop.MainWindow.Loaded += (o, e) => desktop.MainWindow.Close();
                })
                .WithParsed(options =>
                {
                    Logger.Information("Launching app");
                    var factory = new SqlDataStorageFactory();
                    var service = new SqlService(factory.CreateDataStorage());
                    var room = service.FindRoomById(options.RoomId);
                    var stateRoomViewModel = new StateRoomViewModel(room);
                    var mainViewModel = new MainViewModel(stateRoomViewModel, service);
                    _mainWindow = new MainWindow
                    {
                        DataContext = mainViewModel
                    };
                    desktop.MainWindow = _mainWindow;
                });

        }

        base.OnFrameworkInitializationCompleted();
    }
}
