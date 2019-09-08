using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Features.Blogs.CreateBlog;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с блогами
    /// </summary>
    public class BlogController : BaseController
    {
        /// <summary>
        /// Создать
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreateBlogCommand command)
        {
            var result = Mediator.Send<CreateBlogCommand, Result>(command);
            return ResultResponse(result);
        }
    }
}
