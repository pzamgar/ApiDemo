using System;
using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Data;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Infrastructure.Implementation {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository (RepositoryContext context) : base (context) { }

        public async Task<User> AddUserAsync (User user) {
            var result = await FindFirst (u => u.UserName == user.UserName && u.Password == user.Password);
            if (result == null) {
                user.DateCreated = DateTime.UtcNow;
                return await Create (user);
            }
            return null;
        }

        public async Task<User> GetUserAsync (User user) {
            return await FindFirst (u => u.UserName == user.UserName && u.Password == user.Password);
        }

        public async Task<User> GetUserByIdAsync (Guid guid) {
            return await FindFirst (u => u.Id == guid);
        }
    }
}