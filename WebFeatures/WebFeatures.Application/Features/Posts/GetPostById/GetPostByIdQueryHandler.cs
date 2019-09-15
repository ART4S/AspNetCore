using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostEditDto>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PostEditDto Handle(GetPostByIdQuery input)
        {
            var post = _context.Set<Post>()
                .AsNoTracking()
                .First(x => x.Id == input.Id);

            var postDto = _mapper.Map<PostEditDto>(post);

            return postDto;
        }
    }
}
