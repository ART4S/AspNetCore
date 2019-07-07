using DapperContext.QueryProviders.Abstractions;

namespace DapperContext.QueryProviders.Implementations.Sql
{
    public class ProductSqlQueryProvider : IProductQueryProvider
    {
        public string GetAllQuery
            => "SELECT * FROM Products";

        public string GetByIdQuery
            => "SELECT * FROM Products " +
               "WHERE Id = @id";

        public string AddQuery
            => "INSERT INTO Products (Name, Cost)" +
               "VALUES (@Name, @Cost); " +
               "SELECT CAST(SCOPE_IDENTITY() as int);";
        public string RemoveQuery
            => "DELETE FROM Products " +
               "WHERE Id = @id";

        public string MostCostlyProductsQuery
            => string.Empty;
    }
}
