using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Features.Blogs.CreateBlog;
using WebFeatures.Application.Features.Blogs.DeleteBlog;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;
using WebFeatures.WebApi.QueryFilters;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с блогами
    /// </summary>
    public class BlogController : BaseController
    {
        [HttpGet("info")]
        public IActionResult GetAllInfo(QueryFilter filter)
        {
            
        }

        /// <summary>
        /// Создать блог
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreateBlogCommand command)
        {
            var result = Mediator.Send<CreateBlogCommand, Result>(command);
            return ResultResponse(result);
        }

        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="id">Id блога</param>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([Required] int id)
        {
            var result = Mediator.Send<DeleteBlogCommand, Result>(new DeleteBlogCommand() {Id = id});
            return ResultResponse(result);
        }
    }
}
