﻿using AutoMapper;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Result>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result Handle(CreatePostCommand input)
        {
            var post = _mapper.Map<Post>(input);
            _context.Set<Post>().Add(post);

            return Result.Success();
        }
    }
}