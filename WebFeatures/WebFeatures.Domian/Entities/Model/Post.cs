using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пост")]
    public class Post : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
