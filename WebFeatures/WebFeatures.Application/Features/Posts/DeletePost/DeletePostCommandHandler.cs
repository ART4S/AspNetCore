using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Result>
    {
        public Result Handle(DeletePostCommand input)
        {
            throw new System.NotImplementedException();
        }
    }
}
