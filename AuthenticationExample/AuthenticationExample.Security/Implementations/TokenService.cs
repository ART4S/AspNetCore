using AuthenticationExample.Data;
using AuthenticationExample.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthenticationExample.Security.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly UserContext _userContext;

        public TokenService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string GetToken(int userId)
        {
            var user = _userContext.Users.First(x => x.Id == userId);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.Add(AuthenticationDefaults.ExpirationTime).ToString(CultureInfo.InvariantCulture))
            };

            claims.AddRange(user.UserRoles
                .Select(x => x.Role)
                .Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var token = new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    new JwtHeader(
                        new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xXxsuperSecurityKeyxXx")), 
                            SecurityAlgorithms.HmacSha256)), 
                    new JwtPayload(claims)));

            return token;
        }
    }
}
