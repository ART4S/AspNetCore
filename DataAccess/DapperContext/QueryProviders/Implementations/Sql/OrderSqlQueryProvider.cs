using DapperContext.QueryProviders.Abstractions;

namespace DapperContext.QueryProviders.Implementations.Sql
{
    public class OrderSqlQueryProvider : IOrderQueryProvider
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
