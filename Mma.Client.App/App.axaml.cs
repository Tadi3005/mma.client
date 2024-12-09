using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommandLine;
using Mma.Client.Domains;
using Mma.Client.Infrastructures;
using Mma.Client.Infrastructures.Sql;
using Mma.Client.Presentations;
using Mma.Client.Views;
using Serilog;

namespace Mma.Client.App;

public class App : Application
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
                .WithNotParsed(_ =>
                {
                    Logger.Error("Errors in command-line args detected");
                    desktop.MainWindow = new Window();
                    desktop.MainWindow.Loaded += (_,_) => desktop.MainWindow.Close();
                })
                .WithParsed(options =>
                {
                    Logger.Information("Launching app");
                    var connectionString = new ConnectionStringBuilder(options.ConnectionString);
                    var connection = new SqlConnectionManager(connectionString);
                    var factory = new SqlDataStorageFactory(connection);
                    var service = new SqlService(factory.CreateDataStorage());
                    var room = service.FindRoomById(options.RoomId);
                    var reservations = service.FindReservations(DateTime.Now, options.RoomId);
                    var roomState = new RoomState(room, reservations);

                    var stateRoomViewModel = new StateRoomViewModel(roomState);
                    var mainViewModel = new MainViewModel(stateRoomViewModel);

                    _mainWindow = new MainWindow
                    {
                        DataContext = mainViewModel
                    };
                    desktop.MainWindow = _mainWindow;
                    connection.Dispose();
                });

        }

        base.OnFrameworkInitializationCompleted();
    }
}
