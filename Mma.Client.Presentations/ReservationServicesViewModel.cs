using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public class ReservationServicesViewModel(IList<Service> services) : IReservationServicesViewModel
{
    public IReadOnlyCollection<IServiceViewModel> Services => services.Select(service => new ServiceViewModel(service)).ToList();

    public IList<Service> SelectedServices
    {
        get => Services.Where(service => service.IsChecked).Select(service => service.Service).ToList();
    }
}
