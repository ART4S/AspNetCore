using System.Linq;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPostsInfo
{
    public class GetPostInfosQuery : IQuery<IQueryable<PostInfoDto>>
    {
    }
}
