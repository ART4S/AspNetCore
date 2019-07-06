using Model.Entities.Base;

namespace Model.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// Id покупателя
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; }
    }
}
