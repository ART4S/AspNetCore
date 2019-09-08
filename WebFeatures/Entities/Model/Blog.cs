using System.Collections.Generic;
using WebFeatures.Entities.Abstractions;

namespace WebFeatures.Entities.Model
{
    /// <summary>
    /// Блог
    /// </summary>
    public class Blog : BaseEntity<int>
    {
        /// <summary>
        /// Id автора
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL адрес
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Посты
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
    }
}
