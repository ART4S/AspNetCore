using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogsInfoQueryHandler : IQueryHandler<GetBlogsInfoQuery, Result<IQueryable<BlogInfoDto>, Fail>>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetBlogsInfoQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result<IQueryable<BlogInfoDto>, Fail> Handle(GetBlogsInfoQuery input)
        {
            var blogs = _context
                .GetAll<Blog>()
                .ProjectTo<BlogInfoDto>(_mapper.ConfigurationProvider);

            return Result<IQueryable<BlogInfoDto>, Fail>.Success(blogs);
        }
    }
}
