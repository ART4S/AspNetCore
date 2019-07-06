using System.ComponentModel.DataAnnotations;

namespace Model.Entities.Base
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Временной штамп для конкурентного доступа
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
