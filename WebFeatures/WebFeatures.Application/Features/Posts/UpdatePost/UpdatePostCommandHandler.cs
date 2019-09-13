using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Result>
    {
        public Result Handle(UpdatePostCommand input)
        {
            throw new System.NotImplementedException();
        }
    }
}
