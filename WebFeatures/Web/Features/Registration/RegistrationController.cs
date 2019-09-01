using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Features.Registration.RegisterUser;
using Web.Infrastructure.Decorators.Abstractions;
using Web.Infrastructure.Extensions;
using Web.Infrastructure.Results;
using Web.Infrastructure.Validation.Attributes;

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
            [FromBody, Required] RegisterUserCommand command, 
            [FromServices] IHandler<RegisterUserCommand, Result> commandHandler)
        {
            var result = commandHandler.Handle(command);
            return this.ResultResponse(result);
        }
    }
}
