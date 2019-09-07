using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Features.Registration.RegisterUser;
using Web.Infrastructure.Extensions;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Results;
using Web.Infrastructure.Validation.Attributes;

namespace Web.Features.Registration
{
    /// <summary>
    /// Контроллер для регистрации пользователей
    /// </summary>
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        [HttpPost]
        public IActionResult RegisterUser([FromBody, Required] RegisterUserCommand command)
        {
            var result = _mediator.Send<RegisterUserCommand, Result>(command);
            return this.ResultResponse(result);
        }
    }
}
