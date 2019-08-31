using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResultResponse(this Controller controller, Result res)
        {
            if (res.IsSuccess && res.SuccessValue != null)
                return controller.StatusCode(res.StatusCode, res.SuccessValue);

            if (!res.IsSuccess && res.FailureValue != null)
                return controller.StatusCode(res.StatusCode, res.FailureValue);

            return controller.StatusCode(res.StatusCode);
        }
    }
}
