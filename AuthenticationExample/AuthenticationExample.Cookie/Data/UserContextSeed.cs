using AuthenticationExample.Cookie.Data.Model;

namespace AuthenticationExample.Cookie.Data
{
    public static class UserContextSeed
    {
        public static void SeedData(this UserContext context)
        {
            var admin = new User() {Name = "admin"};
            var user = new User() {Name = "user"};

            context.Users.AddRange(admin, user);

            var adminRole = new Role() {Name = "admin"};
            var userRole = new Role() {Name = "user"};

            context.Roles.AddRange(adminRole, userRole);

            context.UserRoles.AddRange(
                new UserRole()
                {
                    User = admin,
                    Role = adminRole,
                },
                new UserRole()
                {
                    User = user,
                    Role = userRole
                });

            context.SaveChanges();
        }
    }
}
