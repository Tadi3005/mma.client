using Mma.Client.Domains;

namespace Mma.Client.Presentations.ViewModel;

public interface IReservationServicesViewModel
{
    IReadOnlyCollection<IServiceViewModel> Services { get; }

    IList<Service> SelectedServices { get; }
}
