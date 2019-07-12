using Model.Entities;

namespace DapperContext.StoredProcedureProviders.Implementations
{
    /// <summary>
    /// Поставщик имен хранимых процедур для таблицы заказов
    /// </summary>
    public class OrderStoredProcedureProvider : IStoredProcedureProvider<Order>
    {
        public string GetAll
            => "SELECT * FROM Orders " +
               "JOIN Customers ON Customers.Id = Orders.CustomerId " +
               "JOIN Products ON Products.Id = Orders.ProductId ";

        public string GetById
            => "SELECT * FROM Orders " +
               "WHERE Id = @id";

        public string Add
            => "INSERT INTO Orders (CustomerId, ProductId)" +
               "VALUES (@CustomerId, @ProductId); " +
               "SELECT CAST(SCOPE_IDENTITY() as int);";

        public string Remove
            => "DELETE FROM Orders " +
               "WHERE Id = @id";
    }
}
