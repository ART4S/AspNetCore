using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FileStoringSample.Data.Context
{
    public interface IDataContext
    {
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
    }
}
