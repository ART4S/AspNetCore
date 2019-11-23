using AuthenticationExample.Identity.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationExample.Identity.Data
{
    public class UserContext : IdentityDbContext<User, Role, string>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
    }
}
