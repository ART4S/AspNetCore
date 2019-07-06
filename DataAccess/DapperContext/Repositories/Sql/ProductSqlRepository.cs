using Dapper;
using Model.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperContext.Repositories.Sql
{
    /// <inheritdoc />
    public class ProductSqlRepository : BaseSqlRepository<Product>
    {
        public ProductSqlRepository(SqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Product> GetAll()
        {
            var query = "SELECT * FROM Products";
            var products = Connection.Query<Product>(query).ToList();

            return products;
        }

        /// <inheritdoc />
        public override Product GetById(int id)
        {
            var query = "SELECT * FROM Products " +
                        "WHERE Id = @id";
            var entity = Connection.QueryFirstOrDefault<Product>(query);

            return entity;
        }

        /// <inheritdoc />
        public override void Add(Product entity)
        {
            var query = "INSERT INTO Products (Name, Cost)" +
                        "VALUES (@Name, @Cost); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = Connection.QueryFirst<int>(query, entity);
            entity.Id = id;
        }

        /// <inheritdoc />
        public override void Remove(int id)
        {
            var query = "DELETE FROM Products " +
                        "WHERE Id = @id";

            Connection.Execute(query, id);
        }
    }
}
