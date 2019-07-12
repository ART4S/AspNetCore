using Dapper;
using DapperContext.StoredProcedureProviders;
using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext.Repositories
{
    /// <summary>
    /// Базовый класс репозитория
    /// </summary>
    public abstract class BaseRepository<TEntity, TStoredProcedureProvider> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TStoredProcedureProvider : IStoredProcedureProvider<TEntity>
    {
        protected readonly IDbConnection Connection;
        protected readonly TStoredProcedureProvider StoredProcedureProvider;

        protected BaseRepository(IDbConnection connection, TStoredProcedureProvider storedProcedureProvider)
        {
            Connection = connection;
            StoredProcedureProvider = storedProcedureProvider;
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetAll()
        {
            var entities = Connection
                .Query<TEntity>(StoredProcedureProvider.GetAll, CommandType.StoredProcedure)
                .ToList();

            return entities;
        }

        /// <inheritdoc />
        public virtual TEntity GetById(int id)
        {
            var entity = Connection.QueryFirstOrDefault<TEntity>(StoredProcedureProvider.GetById, CommandType.StoredProcedure);
            return entity;
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            var id = Connection.QueryFirst<int>(StoredProcedureProvider.Add, entity, commandType: CommandType.StoredProcedure);
            entity.Id = id;
        }

        /// <inheritdoc />
        public virtual void Remove(int id)
        {
            Connection.Execute(StoredProcedureProvider.Remove, id, commandType: CommandType.StoredProcedure);
        }
    }
}
