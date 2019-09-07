using System;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Blogs.CreateBlog
{
    /// <summary>
    /// Обработчик команды на создание блога
    /// </summary>
    public class CreateBlogCommandHandler : ICommandHandler<CreateBlogCommand, Result>
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Result Handle(CreateBlogCommand input)
        {
            throw new NotImplementedException();
        }
    }
}
