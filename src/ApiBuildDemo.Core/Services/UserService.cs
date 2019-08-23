using System;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Services {
    public class UserService : IUserService {
        private readonly ILoggerAdapter<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService (ILoggerAdapter<UserService> logger,
            IUserRepository userRepository,
            IAuthService authService) {
            _logger = logger;
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<string> AddUserAsync (User user) {
            var userRepo = await _userRepository.AddUserAsync (user);
            if (userRepo == null) {
                return "";
            }
            var token = _authService.GenerateToken (userRepo);
            return token;
        }

        public async Task<string> SignInAsync (User user) {
            var userAuthenticated = await _userRepository.GetUserAsync (user);
            if (userAuthenticated == null) {
                return String.Empty;
            }
            var token = _authService.GenerateToken (userAuthenticated);
            return token;
        }
    }
}