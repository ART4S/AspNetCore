using Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Web.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> GetAll<TEntity, TId>(this DbContext dbContext) 
            where TEntity : class, IEntity<TId> 
            where TId : struct
        {
            return dbContext.Set<TEntity>();
        }
    }
}
