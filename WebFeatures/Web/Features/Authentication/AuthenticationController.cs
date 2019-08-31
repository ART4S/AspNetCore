using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Web.Abstractions;
using Web.Extensions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromBody, Required] Login command, 
            [FromServices] IHandler<Login, Result<Claim[], string>> commandHandler)
        {
            var result = commandHandler.Handle(command);
            if (!result.IsSuccess)
                return this.ResultResponse(result);

            var claimsIdentity = new ClaimsIdentity(
                result.SuccessValue, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                }).Wait();

            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok();
        }
    }
}
