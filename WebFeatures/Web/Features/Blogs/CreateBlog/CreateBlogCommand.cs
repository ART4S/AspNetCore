using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Blogs.CreateBlog
{
    /// <summary>
    /// Команда на создание блога
    /// </summary>
    public class CreateBlogCommand : ICommand<Result>
    {
        /// <summary>
        /// Id автора
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Наполнение
        /// </summary>
        public string Content { get; set; }
    }
}
