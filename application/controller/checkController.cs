using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using Microsoft.AspNetCore.Mvc;

namespace API_AUTENTICATION.application.controller
{

    [Route("api/check")]
    public class checkController : ControllerBase
    {
        private readonly IUserService _userService;
        public checkController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Check(string userId)
        {
            try
            {
                 await _userService.CheckserExists(userId);
                return Ok("Usuário ativado com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }

}
