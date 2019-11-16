using AuthenticationExample.Cookie.Data.Abstractions;
using System.Collections.Generic;

namespace AuthenticationExample.Cookie.Data.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }
    }
}
