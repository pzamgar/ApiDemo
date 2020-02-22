using System;
using System.Threading.Tasks;
using ApiBuildDemo.Api.Models;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBuildDemo.Api.Controllers {
    [ApiController]
    [ApiVersion ("1")]
    [Route ("api/v{version:apiVersion}/[controller]")]
    [Produces ("application/json")]
    public class LoginController : ControllerBase {

        private readonly ILoggerAdapter<LoginController> _logger;
        private readonly IUserService _userServices;
        public LoginController (ILoggerAdapter<LoginController> logger, IUserService userService) {
            _logger = logger;
            _userServices = userService;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token authorization</returns>
        /// <remarks>
        /// Login User 
        /// </remarks>
        /// <response code="200">Returns Token JWT</response>
        /// <response code="400">Returns Bad Request</response> 
        /// <response code="500">Error of server</response> 
        [HttpPost ("SignIn")]
        [ProducesResponseType (typeof (UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> SignIn ([FromBody] UserDto user) {

            var result = await _userServices.SignInAsync (new User {
                UserName = user.UserName,
                    Password = user.Password
            });

            if (string.IsNullOrEmpty (result)) {
                return BadRequest ();
            }

            user.Token = result;
            return Ok (user);
        }

        /// <summary>
        /// Sign up User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status code created</returns>
        /// <remarks>
        /// Add User 
        /// </remarks>
        /// <response code="201">Status Created</response>
        /// <response code="400">Returns Bad Request</response> 
        /// <response code="500">Error of server</response> 
        [HttpPost ("AddUser")]
        [ProducesResponseType (StatusCodes.Status201Created)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (UserDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddUser ([FromBody] UserDto user) {

            var result = await _userServices.AddUserAsync (new User {
                UserName = user.UserName,
                    Password = user.Password
            });

            if (string.IsNullOrEmpty (result)) {
                return BadRequest ();
            }

            user.Token = result;
            return Ok (user);
        }

    }
}