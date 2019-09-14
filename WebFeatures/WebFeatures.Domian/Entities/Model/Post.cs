using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Specifications;

namespace WebFeatures.Domian.Entities.Model
{
    [Description("Пост")]
    public class Post : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public static class Specs
        {
            public static readonly Spec<Post> IsVisible = new Spec<Post>(p => p.Content != null && p.Title != null);
        }
    }
}
