using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Web.Features.Authentication.Login;
using Web.Infrastructure.Extensions;
using Web.Infrastructure.Failures;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Results;
using Web.Infrastructure.Validation.Attributes;

namespace Web.Features.Authentication
{
    /// <summary>
    /// Контроллер для работы с аутентификацией
    /// </summary>
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Войти в систему
        /// </summary>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody, Required] LoginCommand command)
        {
            var result = _mediator.Send<LoginCommand, Result<Claim[], Fail>>(command);
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
