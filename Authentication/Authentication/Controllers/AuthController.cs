using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Dto;
using Security.Interfaces;
using Security.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Authentication.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Зарегистрировать аккаунт
        /// </summary>
        /// <param name="registerModel">Форма для регистрации</param>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult RegisterAccount([FromBody, Required] RegisterModel registerModel)
        {
            var registrationResult = _authService.RegisterAccount(registerModel);

            if (!registrationResult.IsSuccess)
            {
                return BadRequest(registrationResult.Message);
            }

            return Ok(registrationResult.Account);
        }

        /// <summary>
        /// Войти в систему
        /// </summary>
        /// <param name="loginModel">Форма для аутентификации</param>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult SignIn([FromBody, Required] LoginModel loginModel)
        {
            var authenticationResult = _authService.GetAccount(loginModel);

            if (!authenticationResult.IsSuccess)
            {
                return BadRequest(authenticationResult.Message);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, authenticationResult.Account.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                }).Wait();

            return Ok(authenticationResult.Account);
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok();
        }
    }
}
