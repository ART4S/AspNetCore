using WebFeatures.Entities.Abstractions;

namespace WebFeatures.Entities.Model
{
    /// <summary>
    /// Пост
    /// </summary>
    public class Post : BaseEntity<int>
    {
        /// <summary>
        /// Id блога
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// Блог
        /// </summary>
        public virtual Blog Blog { get; set; }

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
