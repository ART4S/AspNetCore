using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataContext
{
    /// <summary>
    /// Контекст доступа к данным
    /// </summary>
    public interface IAppContext
    {
        /// <summary>
        /// БД
        /// </summary>
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        DbQuery<T> Query<T>() where T : class;

        int SaveChanges();
    }
}
