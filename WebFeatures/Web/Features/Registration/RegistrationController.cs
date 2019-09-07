using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Features.Registration.RegisterUser;
using Web.Infrastructure.Controllers;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Results;
using Web.Infrastructure.Validation.Attributes;

namespace Web.Features.Registration
{
    /// <summary>
    /// Контроллер для регистрации новых пользователей
    /// </summary>
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public RegistrationController(IMediator mediator) : base(mediator)
        {
        }

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
