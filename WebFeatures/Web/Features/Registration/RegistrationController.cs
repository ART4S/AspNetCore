using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Decorators.Abstractions;
using Web.Extensions;
using Web.Infrastructure;
using Web.Validation.Attributes;

namespace Web.Features.Registration
{
    /// <summary>
    /// Контроллер для регистрации пользователей
    /// </summary>
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        [HttpPost]
        public IActionResult RegisterUser(
            [FromBody, Required] RegisterUser command, 
            [FromServices] IHandler<RegisterUser, Result> commandHandler)
        {
            var result = commandHandler.Handle(command);
            return this.ResultResponse(result);
        }
    }
}
