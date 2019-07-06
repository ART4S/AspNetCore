using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DapperContext.Repositories.Sql
{
    /// <summary>
    /// Базовый класс репозитория SQL
    /// </summary>
    public abstract class BaseSqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly SqlConnection Connection;

        protected BaseSqlRepository(SqlConnection connection)
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
