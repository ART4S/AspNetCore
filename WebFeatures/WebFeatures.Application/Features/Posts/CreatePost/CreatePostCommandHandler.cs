using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Result>
    {
        public Result Handle(CreatePostCommand input)
        {
            throw new System.NotImplementedException();
        }
    }
}
