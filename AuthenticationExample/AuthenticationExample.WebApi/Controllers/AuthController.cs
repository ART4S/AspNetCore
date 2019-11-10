using AuthenticationExample.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationExample.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            return Ok(_tokenService.GetToken(1));
        }

        [HttpPost("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
