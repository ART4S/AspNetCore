using AuthenticationExample.Token.Data;
using AuthenticationExample.Token.Defaults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationExample.Token.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _userContext;

        public AuthController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet("token")]
        public IActionResult GetToken(int userId)
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

            var jwtToken = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        key: AuthenticationDefaults.SecurityKey,
                        algorithm: SecurityAlgorithms.HmacSha256)),
                new JwtPayload(
                    issuer: AuthenticationDefaults.Issuer,
                    audience: AuthenticationDefaults.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(AuthenticationDefaults.ExpirationTime)));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(new
            {
                User = user.Name,
                Token = token
            });
        }
    }
}
