using Mma.Client.Domains;

namespace Mma.Client.Presentations.ViewModel;

public interface IServiceViewModel
{
    string Name { get; set; }

    bool IsChecked { get; }

    Service Service { get; }
}
