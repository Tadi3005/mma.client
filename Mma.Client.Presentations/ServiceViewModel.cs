using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

public class ServiceViewModel(Service service) : IServiceViewModel
{
    public string Name { get; set; } = service.Name;

    public bool IsChecked { get; } = false;

    public Service Service { get; } = service;
}
