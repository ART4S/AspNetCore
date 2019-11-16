using AuthenticationExample.Token.Data.Abstractions;

namespace AuthenticationExample.Token.Data.Model
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
