using authentication_API.domain.dto;
using authentication_API.domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace authentication_API.controller
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
            this._authenticationService = authenticationService;
            this.tokenService = tokenService;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserDto user)
        {
            await userService.AddUser(user);
            return Ok(new { message = "Usuário criado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {


            bool isAuthenticated = await _authenticationService.login(user.Email, user.PasswordHash);

            if (!isAuthenticated)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            string jwtToken = tokenService.GenerateToken(user.Email);

            return Ok(new { Token = jwtToken });
        }
    }
}
