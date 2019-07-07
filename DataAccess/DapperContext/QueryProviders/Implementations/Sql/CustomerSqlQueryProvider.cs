using DapperContext.QueryProviders.Abstractions;

namespace DapperContext.QueryProviders.Implementations.Sql
{
    public class CustomerSqlQueryProvider : ICustomerQueryProvider
    {
        public string GetAllQuery
            => "SELECT * FROM Customers " +
               "LEFT JOIN Orders ON Customers.Id = Orders.CustomerId";

        public string GetByIdQuery
            => "SELECT * FROM Customers " +
               "WHERE Id = @id";

        public string AddQuery
            => "INSERT INTO Customers (Name)" +
               "VALUES (@Name); " +
               "SELECT CAST(SCOPE_IDENTITY() as int);";

        public string RemoveQuery
            => "DELETE FROM Customers " +
               "WHERE Id = @id";

        public string OrderedByOrdersCountCustomersQuery
            => string.Empty;
    }
}
