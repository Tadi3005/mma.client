using Mma.Client.Domains;

namespace Mma.Client.Infrastructures;

public interface IUserDao
{
    IList<User> FindAll();
}
