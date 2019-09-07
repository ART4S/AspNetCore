using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class AppContext : DbContext, IAppContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }
    }
}
