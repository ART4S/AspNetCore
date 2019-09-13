using System.Linq;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogsInfoQuery : IQuery<IQueryable<BlogInfoDto>>
    {
    }
}
