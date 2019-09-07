using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Model;
using Web.Features.Blogs.CreateBlog;

namespace Web.Features.Blogs
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
