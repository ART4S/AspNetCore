using System.Linq;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogsInfoQuery : IQuery<Result<IQueryable<BlogInfoDto>, Fail>>
    {
    }
}
