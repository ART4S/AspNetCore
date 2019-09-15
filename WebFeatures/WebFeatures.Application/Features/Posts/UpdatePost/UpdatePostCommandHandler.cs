using AutoMapper;
using System.Linq;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Result>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result Handle(UpdatePostCommand input)
        {
            var post = _context.Set<Post>().First(x => x.Id == input.Id);
            _mapper.Map(input, post);

            return Result.Success();
        }
    }
}
