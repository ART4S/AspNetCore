using Dapper;
using Model.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperContext.Repositories.Sql
{
    /// <inheritdoc />
    public class OrderSqlRepository : BaseSqlRepository<Order>
    {
        public OrderSqlRepository(SqlConnection connection) : base(connection)
        {
        }

        /// <inheritdoc />
        public override List<Order> GetAll()
        {
            var query = "SELECT * FROM Orders " +
                        "JOIN Customers ON Customers.Id = Orders.CustomerId " +
                        "JOIN Products ON Products.Id = Orders.ProductId ";

            var orders = Connection.Query<Order, Customer, Product, Order>(
                    query,
                    (order, customer, product) =>
                    {
                        order.Product = product;
                        order.Customer = customer;

                        return order;
                    })
                .ToList();

            return orders;
        }

        /// <inheritdoc />
        public override Order GetById(int id)
        {
            var query = "SELECT * FROM Orders " +
                        "WHERE Id = @id";
            var entity = Connection.QueryFirstOrDefault<Order>(query);

            return entity;
        }

        /// <inheritdoc />
        public override void Add(Order entity)
        {
            var query = "INSERT INTO Orders (CustomerId, ProductId)" +
                        "VALUES (@CustomerId, @ProductId); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = Connection.QueryFirst<int>(query, entity);
            entity.Id = id;
        }

        /// <inheritdoc />
        public override void Remove(int id)
        {
            var query = "DELETE FROM Orders " +
                        "WHERE Id = @id";

            Connection.Execute(query, id);
        }
    }
}
