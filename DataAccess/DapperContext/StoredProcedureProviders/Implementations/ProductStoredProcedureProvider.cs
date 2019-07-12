using Model.Entities;

namespace DapperContext.StoredProcedureProviders.Implementations
{
    /// <summary>
    /// Поставщик имен хранимых процедур для таблицы продуктов
    /// </summary>
    public class ProductStoredProcedureProvider : IStoredProcedureProvider<Product>
    {
        public string GetAll
            => "SELECT * FROM Products";

        public string GetById
            => "SELECT * FROM Products " +
               "WHERE Id = @id";

        public string Add
            => "INSERT INTO Products (Name, Cost)" +
               "VALUES (@Name, @Cost); " +
               "SELECT CAST(SCOPE_IDENTITY() as int);";
        public string Remove
            => "DELETE FROM Products " +
               "WHERE Id = @id";

        public string MostCostlyProducts
            => string.Empty;
    }
}
