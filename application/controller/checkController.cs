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

        [HttpPatch("{userId}")]
        public async Task<IActionResult> Check(string userId)
        {
                 await _userService.CheckserExists(userId);
                return Ok("User Acticve.");

        }
    }
}
