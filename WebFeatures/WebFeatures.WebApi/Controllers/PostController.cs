using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Posts.CreatePost;
using WebFeatures.Application.Features.Posts.DeletePost;
using WebFeatures.Application.Features.Posts.GetPostByBlog;
using WebFeatures.Application.Features.Posts.GetPostsInfo;
using WebFeatures.Application.Features.Posts.UpdatePost;
using WebFeatures.QueryFiltering.Extensions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class PostController : BaseController
    {
        [HttpGet("infos")]
        public IActionResult GetInfos(QueryFilter filter)
        {
            var infos = Mediator.Send(new GetPostInfosQuery()).ApplyFilter(filter);
            return Ok(infos);
        }

        [HttpGet]
        public IActionResult GetByBlog([Required] int blogId)
        {
            var post = Mediator.Send(new GetPostByBlogQuery() {BlogId = blogId});
            return ResultResponse(post);
        }

        [HttpPost]
        public IActionResult Create([FromBody, Required] CreatePostCommand command)
        {
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody, Required] UpdatePostCommand command)
        {
            var result = Mediator.Send(command);
            return ResultResponse(result);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([Required] int id)
        {
            var result = Mediator.Send(new DeletePostCommand() {Id = id});
            return ResultResponse(result);
        }
    }
}
