using NSubstitute;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations.Tests
{
    [TestFixture]
    public class ReservationViewModelTests
    {
        private IReservationRequestViewModel _mockReservationRequestViewModel;
        private IReservationStatusViewModel _mockReservationStatusViewModel;
        private ReservationService _mockReservationService;
        private IDataService _mockDataService;
        private Room _mockRoom;

        [SetUp]
        public void SetUp()
        {
            
        }
    }
}
