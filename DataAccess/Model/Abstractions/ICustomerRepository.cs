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
        /// Получить все записи включая зависимости
        /// </summary>
        List<Customer> GetAllWithDependencies();
    }
}
