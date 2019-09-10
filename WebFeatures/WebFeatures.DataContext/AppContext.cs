using Microsoft.EntityFrameworkCore;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common.Time;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext
{
    public abstract class AppContext : DbContext, IAppContext
    {
        protected readonly IServerTime ServerTime;

        protected AppContext(DbContextOptions options, IServerTime serverTime) : base(options)
        {
            ServerTime = serverTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public override int SaveChanges()
        {
            var now = ServerTime.Now;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IUpdatable updatable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            updatable.CreatedAt = now;
                            break;

                        case EntityState.Modified:
                            updatable.UpdatedAt = now;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        #region Tables

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        #endregion
    }
}
