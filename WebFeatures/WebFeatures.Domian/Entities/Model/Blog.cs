using System.Collections.Generic;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    public class Blog : BaseEntity<int>
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Post> Posts { get; private set; }
    }
}
