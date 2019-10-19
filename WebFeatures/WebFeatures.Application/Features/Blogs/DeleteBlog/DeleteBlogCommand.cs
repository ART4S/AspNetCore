using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}
