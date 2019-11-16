using System.Collections.Generic;
using AuthenticationExample.Token.Data.Abstractions;

namespace AuthenticationExample.Token.Data.Model
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
