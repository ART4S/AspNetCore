using System.Linq;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Interfaces
{
    public interface IAppContext
    {
        IQueryable<T> Get<T>(bool tracking = false) where T : class, IEntity, new();

        T GetById<T>(int id) where T : class, IEntity, new();

        void Add<T>(T entity) where T : class, IEntity, new();

        void Remove<T>(int id) where T : class, IEntity, new();

        bool Exists<T>(int id) where T : class, IEntity, new();

        int SaveChanges();
    }
}
