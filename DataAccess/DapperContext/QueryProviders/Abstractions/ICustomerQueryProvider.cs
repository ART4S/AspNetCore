using Model.Entities;

namespace DapperContext.QueryProviders.Abstractions
{
    /// <summary>
    /// Поставщик запросов для таблицы покупателей
    /// </summary>
    public interface ICustomerQueryProvider : IQueryProvider<Customer>
    {
        string OrderedByOrdersCountCustomersQuery { get; }
    }
}
