using AutoMapper;
using Web.Features.Blogs.CreateBlog;
using WebFeatures.Entities.Model;

namespace WebFeatures.WebApi.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandProfile : Profile
    {
        public CreateBlogCommandProfile()
        {
            CreateMap<CreateBlogCommand, Blog>(MemberList.Source);
        }
    }
}
