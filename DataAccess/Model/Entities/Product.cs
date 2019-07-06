using Model.Entities.Base;

namespace Model.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; }
    }
}
