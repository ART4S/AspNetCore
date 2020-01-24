using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FileStoringSample.Context
{
    public interface IDataContext
    {
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
    }
}
