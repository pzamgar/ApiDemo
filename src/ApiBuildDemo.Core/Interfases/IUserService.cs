using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Interfases {
    public interface IUserService {
        Task<string> AddUserAsync (User user);
        Task<string> SignInAsync (User user);
    }
}