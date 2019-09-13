using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogsInfoQueryProfile : Profile
    {
        public GetBlogsInfoQueryProfile()
        {
            CreateMap<Blog, BlogInfoDto>(MemberList.Destination);
        }
    }
}
