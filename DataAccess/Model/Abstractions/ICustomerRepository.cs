using Model.Entities;
using System.Collections.Generic;

namespace Model.Abstractions
{
    /// <summary>
    /// Репозиторий покупателей
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// Получить отсортированный по количеству заказов список покупателей
        /// </summary>
        List<Customer> GetOrderedByOrdersCountCustomers();
    }
}
