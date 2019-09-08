using AutoMapper;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandHandler : ICommandHandler<CreateBlogCommand, Result>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public CreateBlogCommandHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result Handle(CreateBlogCommand input)
        {
            var blog = _mapper.Map<Blog>(input);
            _context.Blogs.Add(blog);

            return Result.Success();
        }
    }
}
