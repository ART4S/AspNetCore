using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Application.Pipeline.Mediators;

namespace WebFeatures.WebApi.Controllers.Base
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Посредник для отправки запроса подходящим обработчикам
        /// </summary>
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        private IMediator _mediator;

        /// <summary>
        /// Ответ с результатом
        /// </summary>
        /// <param name="result">Результат выполнения запроса</param>
        protected IActionResult ResultResponse(Result result)
        {
            if (result.IsSuccess && result.SuccessValue != null)
                return StatusCode(result.StatusCode, result.SuccessValue);

            if (!result.IsSuccess && result.FailureValue != null)
                return StatusCode(result.StatusCode, result.FailureValue);

            return StatusCode(result.StatusCode);
        }
    }
}
