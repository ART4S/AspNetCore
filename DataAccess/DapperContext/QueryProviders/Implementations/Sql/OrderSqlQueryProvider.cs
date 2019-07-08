using DapperContext.QueryProviders.Abstractions;
using Model.Entities;

namespace DapperContext.QueryProviders.Implementations.Sql
{
    public class OrderSqlQueryProvider : IQueryProvider<Order>
    {
        public string GetAllQuery
            => "SELECT * FROM Orders " +
               "JOIN Customers ON Customers.Id = Orders.CustomerId " +
               "JOIN Products ON Products.Id = Orders.ProductId ";

        public string GetByIdQuery
            => "SELECT * FROM Orders " +
               "WHERE Id = @id";

        public string AddQuery
            => "INSERT INTO Orders (CustomerId, ProductId)" +
               "VALUES (@CustomerId, @ProductId); " +
               "SELECT CAST(SCOPE_IDENTITY() as int);";

        public string RemoveQuery
            => "DELETE FROM Orders " +
               "WHERE Id = @id";
    }
}
