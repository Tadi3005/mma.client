using Mma.Client.Domains;

namespace Mma.Client.Infrastructures;

public interface IRoomDao
{
    Room FindById(string roomId);
}
