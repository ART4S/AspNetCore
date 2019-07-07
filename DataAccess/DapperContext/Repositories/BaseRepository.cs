using Dapper;
using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IQueryProvider = DapperContext.QueryProviders.Abstractions.IQueryProvider;

namespace DapperContext.Repositories
{
    /// <summary>
    /// Базовый класс репозитория
    /// </summary>
    public abstract class BaseRepository<TEntity, TQueryProvider> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TQueryProvider : IQueryProvider
    {
        protected readonly IDbConnection Connection;
        protected readonly TQueryProvider QueryProvider;

        protected BaseRepository(IDbConnection connection, TQueryProvider queryProvider)
        {
            Connection = connection;
            QueryProvider = queryProvider;
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetAll()
        {
            var entities = Connection
                .Query<TEntity>(QueryProvider.GetAllQuery)
                .ToList();

            return entities;
        }

        /// <inheritdoc />
        public virtual TEntity GetById(int id)
        {
            var entity = Connection.QueryFirstOrDefault<TEntity>(QueryProvider.GetByIdQuery);
            return entity;
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            var id = Connection.QueryFirst<int>(QueryProvider.AddQuery, entity);
            entity.Id = id;
        }

        /// <inheritdoc />
        public virtual void Remove(int id)
        {
            Connection.Execute(QueryProvider.RemoveQuery, id);
        }
    }
}
