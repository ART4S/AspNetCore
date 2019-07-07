using Model.Entities;
using System.Collections.Generic;

namespace Model.Abstractions
{
    /// <summary>
    /// Репозиторий покупателей
    /// </summary>
    public interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> GetOrderedByOrdersCountCustomers();
    }
}
