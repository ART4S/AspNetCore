using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using WebFeatures.Application.Features.Authentication.Login;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с аутентификацией
    /// </summary>
    public class AuthController : BaseController
    {
        /// <summary>
        /// Войти в систему
        /// </summary>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody, Required] LoginCommand command)
        {
            var result = Mediator.Send<LoginCommand, Result<Claim[], Fail>>(command);
            if (!result.IsSuccess)
                return ResultResponse(result);

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

        /// <summary>
        /// Выйти из системы
        /// </summary>
        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok();
        }
    }
}
