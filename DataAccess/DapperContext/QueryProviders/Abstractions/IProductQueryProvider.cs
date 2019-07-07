namespace DapperContext.QueryProviders.Abstractions
{
    /// <summary>
    /// Поставщик запросов для таблицы продуктов
    /// </summary>
    public interface IProductQueryProvider : IQueryProvider
    {
        string MostCostlyProductsQuery { get; }
    }
}
