using System;
using Microsoft.EntityFrameworkCore;
using WebFeatures.Entities.Abstractions;
using WebFeatures.Entities.Model;

namespace WebFeatures.DataContext
{
    public abstract class AppContext : DbContext, IAppContext
    {
        protected AppContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IUpdatable updatable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            updatable.CreatedAt = utcNow;
                            break;

                        case EntityState.Modified:
                            updatable.UpdatedAt = utcNow;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
