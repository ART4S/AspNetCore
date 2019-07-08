using Model.Entities;

namespace DapperContext.QueryProviders.Abstractions
{
    /// <summary>
    /// Поставщик запросов для таблицы продуктов
    /// </summary>
    public interface IProductQueryProvider : IQueryProvider<Product>
    {
        string MostCostlyProductsQuery { get; }
    }
}
