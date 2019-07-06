using Model.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    [Table("Products")]
    public class Product : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        [Column]
        public decimal Cost { get; set; }
    }
}
