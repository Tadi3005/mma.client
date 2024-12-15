using Mma.Client.Domains;

namespace Mma.Client.Presentations.Tests;

public class ServiceViewModelTest
{
    private Service _service;
    private ServiceViewModel _viewModel;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _service = new Service(1, "Test Service");
        _viewModel = new ServiceViewModel(_service);
    }

    [Test]
    public void Constructor_ShouldInitializeWithCorrectServiceName() =>
        // Assert
        Assert.That(_viewModel.Name, Is.EqualTo("Test Service"));

    [Test]
    public void IsChecked_ShouldBeFalseByDefault() =>
        // Assert
        Assert.That(_viewModel.IsChecked, Is.False);

    [Test]
    public void OnIsCheckedChanged_ShouldUpdateIsCheckedProperty()
    {
        // Arrange
        _viewModel.IsChecked = true;

        // Assert
        Assert.That(_viewModel.IsChecked, Is.True);
    }

    [Test]
    public void Service_ShouldReturnCorrectServiceObject() =>
        // Assert
        Assert.That(_viewModel.Service, Is.EqualTo(_service));
}
