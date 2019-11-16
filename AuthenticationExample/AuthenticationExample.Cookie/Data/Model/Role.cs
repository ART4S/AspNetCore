using System.Collections.Generic;
using AuthenticationExample.Cookie.Data.Abstractions;

namespace AuthenticationExample.Cookie.Data.Model
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }
    }
}
