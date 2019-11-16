using AuthenticationExample.Cookie.Data;
using AuthenticationExample.Cookie.Defaults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationExample.Cookie.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _userContext;

        public AuthController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(int userId)
        {
            var user = _userContext.Users.Single(x => x.Id == userId);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in user.UserRoles.Select(x => x.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            await HttpContext.SignInAsync(
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties()
                {
                    IsPersistent = true, // presists cookie across browser sessions
                    ExpiresUtc = DateTime.UtcNow.Add(AuthenticationDefaults.ExpirationTime)
                });

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
