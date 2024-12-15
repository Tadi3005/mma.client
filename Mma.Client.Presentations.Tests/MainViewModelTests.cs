using Mma.Client.Presentations.ViewModel;
using NSubstitute;

namespace Mma.Client.Presentations.Tests;

public class MainViewModelTests
{
    private IStateRoomViewModel _stateRoomViewModelMock;
    private IDailyScheduleViewModel _dailyScheduleViewModelMock;
    private IReservationViewModel _reservationViewModelMock;
    private MainViewModel _mainViewModel;

    [SetUp]
    public void SetUp()
    {
        // Créer les mocks pour les dépendances
        _stateRoomViewModelMock = Substitute.For<IStateRoomViewModel>();
        _dailyScheduleViewModelMock = Substitute.For<IDailyScheduleViewModel>();
        _reservationViewModelMock = Substitute.For<IReservationViewModel>();

        // Créer l'instance de MainViewModel avec les mocks
        _mainViewModel = new MainViewModel(
            _stateRoomViewModelMock,
            _dailyScheduleViewModelMock,
            _reservationViewModelMock
        );
    }

    [Test]
    public void ShouldInitializeViewModelsCorrectly() =>
        Assert.Multiple(() =>
        {
            Assert.That(_mainViewModel.DailyScheduleViewModel, Is.EqualTo(_dailyScheduleViewModelMock));
            Assert.That(_mainViewModel.ReservationViewModel, Is.EqualTo(_reservationViewModelMock));
            Assert.That(_mainViewModel.StateRoomViewModel, Is.EqualTo(_stateRoomViewModelMock));
        });

    [Test]
    public void StateRoomViewModel_ShouldBeSetCorrectly()
    {
        // Arrange
        var newStateRoomViewModel = Substitute.For<IStateRoomViewModel>();

        // Act
        _mainViewModel.StateRoomViewModel = newStateRoomViewModel;

        // Assert
        Assert.That(_mainViewModel.StateRoomViewModel, Is.EqualTo(newStateRoomViewModel));
    }

    [Test]
    public void StateRoomViewModel_ShouldRaisePropertyChangedEvent()
    {
        // Arrange
        var eventRaised = false;
        _mainViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(_mainViewModel.StateRoomViewModel))
            {
                eventRaised = true;
            }
        };

        // Act
        _mainViewModel.StateRoomViewModel = Substitute.For<IStateRoomViewModel>();

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void DailyScheduleViewModel_ShouldBeSetCorrectly()
    {
        // Arrange
        var newDailyScheduleViewModel = Substitute.For<IDailyScheduleViewModel>();
        var mainViewModel = new MainViewModel(_stateRoomViewModelMock, newDailyScheduleViewModel, _reservationViewModelMock);


        // Assert
        Assert.That(mainViewModel.DailyScheduleViewModel, Is.EqualTo(newDailyScheduleViewModel));
    }
}
