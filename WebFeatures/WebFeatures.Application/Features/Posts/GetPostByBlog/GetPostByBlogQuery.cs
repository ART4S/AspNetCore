using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPostByBlog
{
    public class GetPostByBlogQuery : IQuery<Result<PostDto, Fail>>
    {
        public int BlogId { get; set; }
    }
}
