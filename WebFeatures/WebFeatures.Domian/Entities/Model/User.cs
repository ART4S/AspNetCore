using System.Collections.Generic;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Domian.Entities.Model
{
    public class User : BaseEntity<int>
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Blog> Blogs { get; private set; }
    }
}
