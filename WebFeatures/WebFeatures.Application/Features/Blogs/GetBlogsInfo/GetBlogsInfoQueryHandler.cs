using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogsInfoQueryHandler : IQueryHandler<GetBlogsInfoQuery, IQueryable<BlogInfoDto>>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetBlogsInfoQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<BlogInfoDto> Handle(GetBlogsInfoQuery input)
        {
            var blogs = _context
                .GetAll<Blog>()
                .ProjectTo<BlogInfoDto>(_mapper.ConfigurationProvider);

            return blogs;
        }
    }
}
