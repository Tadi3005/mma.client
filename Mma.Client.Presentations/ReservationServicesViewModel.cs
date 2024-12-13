using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public class ReservationServicesViewModel : ObservableObject, IReservationServicesViewModel
{
    public ReservationServicesViewModel(IList<Service> services)
    {
        _services = new ObservableCollection<ServiceViewModel>(
            services.Select(service => new ServiceViewModel(service)));
    }

    private readonly ObservableCollection<ServiceViewModel> _services;

    public IReadOnlyCollection<IServiceViewModel> Services => _services;

    public IList<Service> SelectedServices =>
        _services
            .Where(serviceViewModel => serviceViewModel.IsChecked)
            .Select(serviceViewModel => serviceViewModel.Service)
            .ToList();
}
