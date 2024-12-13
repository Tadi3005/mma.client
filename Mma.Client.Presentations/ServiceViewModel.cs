using CommunityToolkit.Mvvm.ComponentModel;
using Mma.Client.Domains;
using Mma.Client.Presentations.ViewModel;

namespace Mma.Client.Presentations;

/**
 * <summary>
 * Represents a service view model.
 * </summary>
 */
public partial class ServiceViewModel(Service service) : ObservableObject, IServiceViewModel
{
    public string Name { get; set; } = service.Name;

    public Service Service => service;

    [ObservableProperty]
    private bool _isChecked;

    partial void OnIsCheckedChanged(bool value) => _isChecked = value;
}
