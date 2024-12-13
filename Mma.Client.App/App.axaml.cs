using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommandLine;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.AskReservation.Validator;
using Mma.Client.Infrastructures;
using Mma.Client.Infrastructures.Sql;
using Mma.Client.Presentations;
using Mma.Client.Presentations.ViewModel;
using Mma.Client.Views;
using MySql.Data.MySqlClient;
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
    private readonly Timer _refreshTimer = new(TimeSpan.FromMinutes(5).TotalMilliseconds); // TODO: DispatcherTimer

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(desktop.Args)
                    .WithNotParsed(_ =>
                    {
                        Logger.Error("Errors in command-line args detected");
                        desktop.MainWindow = new Window();
                        desktop.MainWindow.Loaded += (_, _) => desktop.MainWindow.Close();
                    })
                    .WithParsed(options =>
                    {
                        Logger.Information("Launching app");

                        const string provider = "MySql.Data.MySqlClient";
                        var connectionStringBuilder = new ConnectionStringBuilder(options.ConnectionString);
                        DbProviderFactories.RegisterFactory(provider, MySqlClientFactory.Instance);
                        var connectionString = $"Server={connectionStringBuilder.DbServer};Port={connectionStringBuilder.DbPort};Database={connectionStringBuilder.DbDataBase};User Id={connectionStringBuilder.DbUser};Password={connectionStringBuilder.DbPassword}";
                        var connection = DbProviderFactories.GetFactory(provider).CreateConnection();

                        if (connection == null)
                        {
                            throw new InvalidOperationException("Connection is null");
                        }

                        connection.ConnectionString = connectionString;
                        connection.Open();

                        var factory = new SqlDataStorageFactory();
                        var service = new SqlService(factory.CreateDataStorage(connection));
                        var openingHours = new OpeningHours(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
                        var room = service.FindRoomById(options.RoomId);
                        var users = service.FindAllUsers();
                        var reservations = service.FindReservations(DateTime.Now, options.RoomId);
                        var services = service.FindAllServices();
                        var roomState = new RoomState(room, reservations);
                        var dailyScheduleService = new ScheduleService(new SlotGenerator(openingHours));
                        var reservationService =
                            new ReservationService(
                                new List<IReservationValidator>
                                {
                                    new UserIdValidator(),
                                    new RoomCapacityValidator(),
                                    new RoomAvailabilityValidator(),
                                    new EndBeforeStartValidator(),
                                    new OutsideOpeningHoursValidator(openingHours),
                                    new PastDateValidator(),
                                    new HoursExactlyValidator(),
                                    new DescriptionValidator()
                                }, users, room);

                        // ViewModel
                        var stateActualRoomViewModel = new ActualStateRoomViewModel(roomState);
                        var stateRoomViewModel = new StateRoomViewModel(stateActualRoomViewModel, roomState);
                        var slotViewModels = new List<ISlotViewModel>();
                        var dailyScheduleViewModel = new DailyScheduleViewModel(slotViewModels, dailyScheduleService, room, service);
                        var statusViewModel = new ReservationStatusViewModel(ReservationStatus.None);
                        var dateTimeReservationRequestViewModel = new DateTimeReservationRequestViewModel();
                        var reservationSercicesViewModel = new ReservationServicesViewModel(services);
                        var reservationRequestViewModel = new ReservationRequestViewModel(dateTimeReservationRequestViewModel, reservationSercicesViewModel);
                        var reservationViewModel = new ReservationViewModel(statusViewModel, reservationRequestViewModel, reservationService, service, room);
                        var mainViewModel = new MainViewModel(stateRoomViewModel, dailyScheduleViewModel, reservationViewModel);

                        _mainWindow = new MainWindow { DataContext = mainViewModel };
                        desktop.MainWindow = _mainWindow;

                        _refreshTimer.Elapsed += (_, _) =>
                        {
                            var refreshRoomState = new RoomState(room, service.FindReservations(DateTime.Now, options.RoomId));
                            mainViewModel.StateRoomViewModel = new StateRoomViewModel(new ActualStateRoomViewModel(refreshRoomState), refreshRoomState);
                        };
                        _refreshTimer.Start();
                    });
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occurred during application initialization: {ex.Message}");
                desktop.MainWindow?.Close();
            }
        }
        base.OnFrameworkInitializationCompleted();
    }
}
