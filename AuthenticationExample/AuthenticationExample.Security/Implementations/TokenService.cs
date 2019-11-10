using AuthenticationExample.Data;
using AuthenticationExample.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationExample.Security.Implementations
{
    internal class TokenService : ITokenService
    {
        private readonly UserContext _userContext;

        public TokenService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string GetToken(int userId)
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

            return token;
        }
    }
}
