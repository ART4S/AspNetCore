using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Interfaces
{
    public interface IAppContext
    {
        DatabaseFacade Database { get; }

        DbSet<User> Users { get; set; }

        DbSet<Blog> Blogs { get; set; }

        DbSet<Post> Posts { get; set; }

        int SaveChanges();
    }
}
