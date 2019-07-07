using Model.Entities;
using System.Collections.Generic;

namespace Model.Abstractions
{
    /// <summary>
    /// Репозиторий продуктов
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Получить список самых дорогих продуктов
        /// </summary>
        List<Product> GetMostCostlyProducts();
    }
}
