using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    /// <summary>
    /// Контроллер для проверки работоспособности аутентификации
    /// </summary>
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
