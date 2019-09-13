using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPostByBlog
{
    public class GetPostByBlogQueryHandler : IQueryHandler<GetPostByBlogQuery, Result<PostDto, Fail>>
    {
        public Result<PostDto, Fail> Handle(GetPostByBlogQuery input)
        {
            throw new System.NotImplementedException();
        }
    }
}
