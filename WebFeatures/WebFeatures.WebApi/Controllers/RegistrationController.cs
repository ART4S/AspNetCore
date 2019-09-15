using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Features.Registration.RegisterUser;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Регистрация новых пользователей
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
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }
    }
}
