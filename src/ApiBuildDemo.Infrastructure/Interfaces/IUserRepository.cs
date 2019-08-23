using System;
using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Infrastructure.Interfaces {
    public interface IUserRepository : IRepositoryBase<User> {
        Task<User> GetUserAsync (User user);
        Task<User> AddUserAsync (User user);
        Task<User> GetUserByIdAsync (Guid guid);
    }
}