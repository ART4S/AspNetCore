using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Controllers
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Посредник для отправки запроса подходящим обработчикам
        /// </summary>
        protected readonly IMediator Mediator;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Универсальный ответ
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
