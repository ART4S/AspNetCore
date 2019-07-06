using Model.Entities.Base;
using System.Collections.Generic;

namespace Model.Entities
{
    /// <summary>
    /// Покупатель
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
