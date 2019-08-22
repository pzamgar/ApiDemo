using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Services {
    public class UserService : IUserService {
        private readonly ILoggerAdapter<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService (ILoggerAdapter<UserService> logger,
            IUserRepository userRepository) {
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<string> AddUserAsync (User user) {
            var result = await _userRepository.AddUser (user);
            if (result == null) {
                return "";
            }
            return "ok";
        }

        public async Task<string> SignInAsync (User user) {
            var result = await _userRepository.GetUser (user);
            if (result == null) {
                return "";
            }
            return "ok";
        }
    }
}