using AuthenticationExample.Data;
using AuthenticationExample.Security.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationExample.Security.Implementations
{
    internal class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserContext _userContext;

        public CookieService(IHttpContextAccessor httpContextAccessor, UserContext userContext)
        {
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SignIn(int userId)
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

            _httpContextAccessor.HttpContext.SignInAsync(
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), 
                new AuthenticationProperties()
                {
                    IsPersistent = true, // presists cookie across browser sessions
                    ExpiresUtc = DateTime.UtcNow.Add(AuthenticationDefaults.ExpirationTime)
                });
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
