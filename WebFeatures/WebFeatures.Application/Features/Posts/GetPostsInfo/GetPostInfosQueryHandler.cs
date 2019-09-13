using System;
using System.Linq;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Posts.GetPostsInfo
{
    public class GetPostInfosQueryHandler : IQueryHandler<GetPostInfosQuery, IQueryable<PostInfoDto>>
    {
        public IQueryable<PostInfoDto> Handle(GetPostInfosQuery input)
        {
            throw new NotImplementedException();
        }
    }
}
