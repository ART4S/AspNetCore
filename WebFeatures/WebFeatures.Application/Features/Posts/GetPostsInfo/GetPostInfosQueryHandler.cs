﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostsInfo
{
    public class GetPostInfosQueryHandler : IQueryHandler<GetPostInfosQuery, IQueryable<PostInfoDto>>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetPostInfosQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<PostInfoDto> Handle(GetPostInfosQuery input)
        {
            var infos = _context
                .Set<Post>()
                .AsNoTracking()
                .Where(Post.Specs.IsVisible)
                .ProjectTo<PostInfoDto>(_mapper.ConfigurationProvider);

            return infos;
        }
    }
}
