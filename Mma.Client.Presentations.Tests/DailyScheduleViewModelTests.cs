using Mma.Client.Domains;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;
using NSubstitute;

namespace Mma.Client.Presentations.Tests;

[TestFixture]
public class DailyScheduleViewModelTests
{
    private ScheduleService _dailyScheduleService;
    private IDataService _dataService;
    private Room _room;
    private List<ISlotViewModel> _initialSlotViewModels;

    [SetUp]
    public void SetUp()
    {
        var openingHours = new OpeningHours(
            start: TimeSpan.FromHours(8),
            end: TimeSpan.FromHours(18)
        );

        var slotGenerator = new SlotGenerator(openingHours);

        _dailyScheduleService = new ScheduleService(slotGenerator);

        _dataService = Substitute.For<IDataService>();
        _room = new Room("Room123", "Test Room", 10);
        _initialSlotViewModels =
        [
            Substitute.For<ISlotViewModel>(),
            Substitute.For<ISlotViewModel>()
        ];
    }

    [Test]
    public void OnSelectedDateChanged_ShouldClearSlotsAndSetErrorMessage_WhenDateIsSaturday()
    {
        // Arrange
        var today = DateTime.Now;
        var daysToSaturday = DayOfWeek.Saturday - today.DayOfWeek;
        if (daysToSaturday < 0)
        {
            daysToSaturday += 7;
        }

        var saturdayThisWeek = today.AddDays(daysToSaturday);

        var viewModel = new DailyScheduleViewModel(
            _initialSlotViewModels,
            _dailyScheduleService,
            _room,
            _dataService
        ) {
            // Act
            SelectedDate = saturdayThisWeek.ToString("yyyy-MM-dd") };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Slots, Is.Empty, "Les créneaux devraient être vides pour un week-end.");
            Assert.That(viewModel.MessageError, Is.EqualTo("Impossible de réserver un week-end"), "Message d'erreur incorrect.");
        });
    }

    [Test]
    public void OnSelectedDateChanged_ShouldClearSlotsAndSetErrorMessage_WhenDateIsSunday()
    {
        // Arrange
        var today = DateTime.Now;
        var daysToNextSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
        if (daysToNextSunday == 0)
        {
            daysToNextSunday = 7;
        }

        var nextSunday = today.AddDays(daysToNextSunday);


        var viewModel = new DailyScheduleViewModel(
            _initialSlotViewModels,
            _dailyScheduleService,
            _room,
            _dataService
        ) {
            // Act
            SelectedDate = nextSunday.ToString("yyyy-MM-dd") };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Slots, Is.Empty, "Les créneaux devraient être vides pour un week-end.");
            Assert.That(viewModel.MessageError, Is.EqualTo("Impossible de réserver un week-end"), "Message d'erreur incorrect.");
        });
    }

}

