using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Features.Registration.RegisterUser;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для регистрации новых пользователей
    /// </summary>
    [AllowAnonymous]
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        [HttpPost]
        public IActionResult RegisterUser([FromBody, Required] RegisterUserCommand command)
        {
            var result = Mediator.Send<RegisterUserCommand, Result>(command);
            return ResultResponse(result);
        }
    }
}
