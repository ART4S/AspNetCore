using Microsoft.EntityFrameworkCore;
using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Linq;

namespace EFContext.Repositories
{
    /// <inheritdoc />
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected AppContext Context { get; }

        protected DbSet<TEntity> Table => Context.Set<TEntity>();

        public BaseRepository(AppContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetAll()
        {
            return Table.AsNoTracking().ToList();
        }

        /// <inheritdoc />
        public virtual TEntity GetById(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            Table.Add(entity);
            Context.SaveChanges();
        }

        /// <inheritdoc />
        public virtual void Remove(int id)
        {
            var entity = new TEntity {Id = id};

            Table.Remove(entity);
            Context.SaveChanges();
        }
    }
}
