using System.Collections.Generic;
using AuthenticationExample.Data.Abstractions;

namespace AuthenticationExample.Data.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; }
    }
}
