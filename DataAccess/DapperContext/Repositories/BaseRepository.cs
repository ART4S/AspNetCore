using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data;

namespace DapperContext.Repositories
{
    /// <inheritdoc />
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IDbConnection Connection;

        protected BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        /// <inheritdoc />
        public abstract List<TEntity> GetAll();

        /// <inheritdoc />
        public abstract TEntity GetById(int id);

        /// <inheritdoc />
        public abstract void Add(TEntity entity);

        /// <inheritdoc />
        public abstract void Remove(int id);
    }
}
