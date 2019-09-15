using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Posts.CreatePost;
using WebFeatures.Application.Features.Posts.DeletePost;
using WebFeatures.Application.Features.Posts.GetPostById;
using WebFeatures.Application.Features.Posts.GetPostsInfo;
using WebFeatures.Application.Features.Posts.UpdatePost;
using WebFeatures.QueryFiltering.Extensions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Посты
    /// </summary>
    public class PostController : BaseController
    {
        /// <summary>
        /// Получить информацию по всем постам
        /// </summary>
        /// <param name="filter">Фильтр</param>
        [HttpGet("infos")]
        public IActionResult GetInfos(QueryFilter filter)
        {
            var infos = Mediator.Send(new GetPostInfosQuery()).ApplyFilter(filter);
            return Ok(infos);
        }

        /// <summary>
        /// Получить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpGet("{id:int}")]
        public IActionResult GetById([Required] int id)
        {
            var post = Mediator.Send(new GetPostByIdQuery() {Id = id});
            return Ok(post);
        }

        /// <summary>
        /// Создать пост
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreatePostCommand command)
        {
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }

        /// <summary>
        /// Редактировать пост
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody, Required] UpdatePostCommand command)
        {
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete([Required] int id)
        {
            var result = Mediator.Send(new DeletePostCommand() {Id = id});
            return ResultResponse(result);
        }
    }
}
