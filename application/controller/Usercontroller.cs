using API_AUTENTICATION.application.dto;
using API_AUTENTICATION.application.Service;
using API_AUTENTICATION.domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_AUTENTICATION.application.controller
{
    [Route("api/[controller]")]
    public class Usercontroller : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
       
        public Usercontroller(IUserService userService, IAuthenticationService authenticationService, ITokenService tokenService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _tokenService = tokenService;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserRequestDto userRequest)
        {
            await _userService.AddUser(userRequest);
            return StatusCode(201, new
            {
                message = "Registration successfuly",
            
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto userRequest)
        {
            bool isAuthenticated = await _authenticationService.login(userRequest.Email, userRequest.PasswordHash);

            if (!isAuthenticated)
            {
                return Unauthorized(new { message = "password or username Invalid." });
            }

            string jwtToken = _tokenService.GenerateToken(userRequest.Email);

            return Ok(new { Token = jwtToken });
        }

        [HttpGet("healthCheck")]
        public async Task<IActionResult> healthCheck()
        {
            return Ok("API is running");
        }
    }
}
