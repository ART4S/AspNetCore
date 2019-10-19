using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommandHandler : ICommandHandler<DeleteBlogCommand, Unit>
    {
        private readonly IAppContext _context;

        public DeleteBlogCommandHandler(IAppContext context)
        {
            _context = context;
        }

        public Unit Handle(DeleteBlogCommand input)
        {
            _context.Set<Blog>().Remove(input.Id);
            return Unit.Value;
        }
    }
}
