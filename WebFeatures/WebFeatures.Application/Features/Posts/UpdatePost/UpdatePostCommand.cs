using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommand : ICommand<Result>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
