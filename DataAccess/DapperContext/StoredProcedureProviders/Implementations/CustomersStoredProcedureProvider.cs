namespace DapperContext.StoredProcedureProviders.Implementations
{
    /// <summary>
    /// Поставщик имен хранимых процедур для таблицы покупателей
    /// </summary>
    public class CustomersStoredProcedureProvider : IStoredProcedureProvider
    {
        public string GetAll => "Customers_GetAll";

        public string GetById => "Customers_GetById";

        public string Add => "Customers_Add";

        public string Remove => "Customers_Remove";

        public string OrderedByOrdersCountCustomers => string.Empty;
    }
}
