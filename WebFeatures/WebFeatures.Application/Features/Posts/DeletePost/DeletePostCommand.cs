using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.DeletePost
{
    public class DeletePostCommand : ICommand<Result>
    {
        public int Id { get; set; }
    }
}
