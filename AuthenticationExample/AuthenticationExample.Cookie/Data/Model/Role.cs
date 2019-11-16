using System.Collections.Generic;

namespace AuthenticationExample.Cookie.Data.Model
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }
    }
}
