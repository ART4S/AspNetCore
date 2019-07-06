using Dapper;
using DapperContext.Information;
using DapperContext.Queries.Sql;
using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperContext
{
    /// <inheritdoc />
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IDbConnection Connection;
        protected readonly TableInfo Table = TableInfoProvider.GetTableInfo(typeof(TEntity));

        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        /// <inheritdoc />
        public virtual List<TEntity> GetAll()
        {
            var query = new SqlSelectQuery(Table).All();

            return Connection
                .Query<TEntity>(query)
                .ToList();
        }

        /// <inheritdoc />
        public virtual TEntity GetById(int id)
        {
            var query = new SqlSelectQuery(Table).ById();
            var entity = Connection.QueryFirstOrDefault<TEntity>(query, new { Id = id });

            return entity;
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            var query = new SqlInsertQuery(Table).InsertAndReturnId();

            var id = Connection
                .Query<int>(query, entity)
                .FirstOrDefault();

            entity.Id = id;
        }

        /// <inheritdoc />
        public virtual void Remove(int id)
        {
            var query = new SqlDeleteQuery(Table).DeleteById();
            Connection.Execute(query, id);
        }
    }
}
