using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Result>
    {
        private readonly IAppContext _context;

        public DeletePostCommandHandler(IAppContext context)
        {
            _context = context;
        }

        public Result Handle(DeletePostCommand input)
        {
            _context.Set<Post>().Remove(input.Id);
            return Result.Success();
        }
    }
}
