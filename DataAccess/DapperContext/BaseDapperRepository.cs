using Dapper;
using Model.Abstractions;
using Model.Entities.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IQueryProvider = DapperContext.QueryProvider.Abstractions.IQueryProvider;

namespace DapperContext
{
    public class BaseDapperRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IDbConnection _connection;
        private readonly IQueryProvider _queryProvider;

        public BaseDapperRepository(IDbConnection connection, IQueryProvider queryProvider)
        {
            _connection = connection;
            _queryProvider = queryProvider;
        }

        public List<TEntity> GetAll()
        {
            var sql = _queryProvider.Select().All();

            return _connection
                .Query<TEntity>(sql)
                .ToList();
        }

        public TEntity GetById(int id)
        {
            var sql = _queryProvider.Select().ById();
            var entity = _connection.QueryFirstOrDefault<TEntity>(sql, id);

            return entity;
        }

        public void Add(TEntity entity)
        {
        }

        public void Remove(int id)
        {
        }
    }
}
