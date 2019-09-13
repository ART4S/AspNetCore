using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Extensions
{
    static class AppContextExtensions
    {
        public static void Remove<TEntity, TId>(this DbSet<TEntity> set, TId id)
            where TEntity : class, IEntity<TId>
            where TId : struct
        {
            var entity = set.First(x => Equals(x.Id, id));
            set.Remove(entity);
        }

        public static IQueryable<TEntity> GetAll<TEntity>(this IAppContext context, bool tracking = false) where TEntity : class
            => (tracking ? context.Set<TEntity>() : context.Set<TEntity>().AsNoTracking());
    }
}
