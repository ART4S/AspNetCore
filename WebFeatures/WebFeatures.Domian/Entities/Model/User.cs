using System.Collections.Generic;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities.Model
{
    public class User : BaseEntity<int>
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
        }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public virtual ContactDetails ContactDetails { get; set; }

        public virtual ICollection<Blog> Blogs { get; private set; }
    }
}
