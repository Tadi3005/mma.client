using Ai.Csharp.Stub.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Serilog;

namespace Ai.Csharp.Stub.App;

public partial class App : Application
{
    private MainWindow? _mainWindow;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _mainWindow = new MainWindow() { Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("log.txt").CreateLogger() };
            _mainWindow.Loaded += (obj, evt) => _mainWindow.DoSomething();
            desktop.MainWindow = _mainWindow;

        }

        base.OnFrameworkInitializationCompleted();
    }
}
