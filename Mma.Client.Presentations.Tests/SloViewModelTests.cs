using Mma.Client.Domains;

namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class SlotViewModelTests
    {
        private SlotViewModel _viewModel;
        private Slot _slot;

        [SetUp]
        public void SetUp()
        {
            // Création d'un slot de test
            var slotStart = DateTime.Now;
            var slotEnd = slotStart.AddMinutes(30);
            _slot = new Slot(slotStart, slotEnd, true); // Exemple de slot libre

            // Création du SlotViewModel avec le slot
            _viewModel = new SlotViewModel(_slot);
        }

        [Test]
        public void TimeEnd_ShouldBeFormattedCorrectly()
        {
            // Act
            var timeEnd = _viewModel.TimeEnd;

            // Assert
            Assert.That(timeEnd, Is.EqualTo(_slot.End.ToString("HH:mm")));
        }

        [Test]
        public void HexColorBackground_ShouldBeGreen_WhenSlotIsFree()
        {
            // Act
            var hexColorBackground = _viewModel.HexColorBackground;

            // Assert
            Assert.That(hexColorBackground, Is.EqualTo("#00ffa1"));
        }

        [Test]
        public void HexColorBackground_ShouldBeRed_WhenSlotIsNotFree()
        {
            // Arrange : Modifier l'état du slot pour qu'il ne soit pas libre
            _slot = _slot with { IsFree = false };
            _viewModel = new SlotViewModel(_slot);

            // Act
            var hexColorBackground = _viewModel.HexColorBackground;

            // Assert
            Assert.That(hexColorBackground, Is.EqualTo("#ff01a0"));
        }

        [Test]
        public void SetTimeEnd_ShouldUpdateTimeEndProperty()
        {
            // Arrange
            var newTimeEnd = DateTime.Now.AddMinutes(45).ToString("HH:mm");

            // Act
            _viewModel.TimeEnd = newTimeEnd;

            // Assert
            Assert.That(_viewModel.TimeEnd, Is.EqualTo(newTimeEnd));
        }

        [Test]
        public void SetHexColorBackground_ShouldUpdateHexColorBackgroundProperty()
        {
            // Arrange
            const string newHexColor = "#ff5733";

            // Act
            _viewModel.HexColorBackground = newHexColor;

            // Assert
            Assert.That(_viewModel.HexColorBackground, Is.EqualTo(newHexColor));
        }
    }
}
