using Model.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    [Table("Orders")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// Id покупателя
        /// </summary>
        [Column]
        public int CustomerId { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Id продукта
        /// </summary>
        [Column]
        public int ProductId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; }
    }
}
