using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommand : ICommand<Result>
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
    }
}
