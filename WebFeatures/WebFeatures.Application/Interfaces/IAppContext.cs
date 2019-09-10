using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebFeatures.Application.Interfaces
{
    public interface IAppContext
    {
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
    }
}
