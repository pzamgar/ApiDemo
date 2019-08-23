using ApiBuildDemo.Infrastructure.Models;

namespace ApiBuildDemo.Core.Interfases {
    public interface IAuthService {
        string GenerateToken (User user);
    }
}