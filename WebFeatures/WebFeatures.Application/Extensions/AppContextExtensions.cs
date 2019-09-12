using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Extensions
{
    static class AppContextExtensions
    {
        public static void Remove<TEntity, TId>(this IAppContext context, TId id)
            where TEntity : class, IEntity<TId>
            where TId : struct
        {
            var entity = context.Set<TEntity>().First(x => Equals(x.Id, id));
            context.Set<TEntity>().Remove(entity);
        }

        public static IQueryable<TEntity> GetAll<TEntity>(this IAppContext context, bool tracking = false) where TEntity : class
            => (tracking ? context.Set<TEntity>() : context.Set<TEntity>().AsNoTracking());
    }
}
