using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Features.Blogs.CreateBlog;
using WebFeatures.Application.Features.Blogs.DeleteBlog;
using WebFeatures.Application.Features.Blogs.GetBlogsInfo;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Utils;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Блоги
    /// </summary>
    public class BlogController : BaseController
    {
        /// <summary>
        /// Получить информацию по всем блогам
        /// </summary>
        [HttpGet("infos")]
        public IActionResult GetInfos(QueryFilter filter)
        {
            var infos = Mediator.Send(new GetBlogInfosQuery()).ApplyFilter(filter);
            return Ok(infos);
        }

        /// <summary>
        /// Создать блог
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreateBlogCommand command)
        {
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }

        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="id">Id блога</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete([Required] int id)
        {
            var result = Mediator.Send(new DeleteBlogCommand() {Id = id});
            return ResultResponse(result);
        }
    }
}
