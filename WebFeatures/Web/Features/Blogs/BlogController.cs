using Microsoft.AspNetCore.Mvc;
using Web.Features.Blogs.CreateBlog;
using Web.Infrastructure.Controllers;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Results;

namespace Web.Features.Blogs
{
    /// <summary>
    /// Контроллер для работы с блогами
    /// </summary>
    [Route("api/v1/[controller]")]
    public class BlogController : BaseController
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public BlogController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Создать
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] CreateBlogCommand command)
        {
            var result = Mediator.Send<CreateBlogCommand, Result>(command);
            return ResultResponse(result);
        }
    }
}
