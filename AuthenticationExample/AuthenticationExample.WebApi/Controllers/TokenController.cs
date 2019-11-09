using AuthenticationExample.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationExample.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult GetToken()
        {
            return Ok(new
            {
                access_token = _tokenService.GetToken(1)
            });
        }
    }
}
