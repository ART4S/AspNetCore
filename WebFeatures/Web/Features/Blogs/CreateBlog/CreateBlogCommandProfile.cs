using AutoMapper;
using Entities.Model;

namespace Web.Features.Blogs.CreateBlog
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class CreateBlogCommandProfile : Profile
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public CreateBlogCommandProfile()
        {
            CreateMap<CreateBlogCommand, Blog>(MemberList.Source);
        }
    }
}
