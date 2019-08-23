using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Core.Options;
using ApiBuildDemo.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiBuildDemo.Core.Services {
    public class AuthService : IAuthService {
        private readonly ILoggerAdapter<AuthService> _logger;
        private readonly AuthSettings _authSettings;

        public AuthService (ILoggerAdapter<AuthService> logger, IOptions<AuthSettings> authSettings) {
            _logger = logger;
            _authSettings = authSettings.Value;
        }

        public string GenerateToken (User user) {

            var key = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (_authSettings.Secret));
            var jwtToken = new JwtSecurityToken (
                claims: new Claim[] {
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                        new Claim (ClaimTypes.Name, user.UserName)
                },
                expires : DateTime.Now.AddMinutes (_authSettings.Expires),
                signingCredentials : new SigningCredentials (key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler ().WriteToken (jwtToken);
        }
    }
}