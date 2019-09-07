using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataContext
{
    public interface IAppContext
    {
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        DbQuery<T> Query<T>() where T : class;

        int SaveChanges();
    }
}
