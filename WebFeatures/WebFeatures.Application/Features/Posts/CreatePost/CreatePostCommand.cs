using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommand : ICommand<Result>
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
