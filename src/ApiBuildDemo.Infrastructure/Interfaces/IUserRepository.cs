using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Infrastructure.Interfaces
{
    public interface IUserRepository: IRepositoryBase<User>
    {
         Task<User> GetUser(User user);
         Task<User> AddUser(User user);
    }
}