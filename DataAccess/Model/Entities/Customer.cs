using Model.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    /// <summary>
    /// Покупатель
    /// </summary>
    [Table("Customers")]
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
