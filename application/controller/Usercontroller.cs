using API_AUTENTICATION.domain.Service;
using authentication_API.domain.dto;
using Microsoft.AspNetCore.Mvc;

namespace API_AUTENTICATION.application.controller
{
    [Route("api/[controller]")]
    public class Usercontroller : ControllerBase
    {
        private readonly UserService userService;
        private readonly authenticationService _authenticationService;
        private readonly TokenService tokenService;
        public Usercontroller(UserService userService, authenticationService authenticationService, TokenService tokenService)
        {
            this.userService = userService;
            _authenticationService = authenticationService;
            this.tokenService = tokenService;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserDto user)
        {
            await userService.AddUser(user);
            return Created();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {

            bool isAuthenticated = await _authenticationService.login(user.Email, user.PasswordHash);

            if (!isAuthenticated)
            {
                return Unauthorized("password or username Invalid.");
            }

            string jwtToken = tokenService.GenerateToken(user.Email);

            return Ok(new { Token = jwtToken });
        }
    }
}
