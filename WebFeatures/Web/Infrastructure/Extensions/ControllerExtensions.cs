using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Extensions
{
    /// <summary>
    /// Расширения для контроллера
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Универсальный ответ сервера
        /// </summary>
        public static IActionResult ResultResponse(this Controller controller, Result result)
        {
            if (result.IsSuccess && result.SuccessValue != null)
                return controller.StatusCode(result.StatusCode, result.SuccessValue);

            if (!result.IsSuccess && result.FailureValue != null)
                return controller.StatusCode(result.StatusCode, result.FailureValue);

            return controller.StatusCode(result.StatusCode);
        }
    }
}
