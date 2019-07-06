using System.ComponentModel.DataAnnotations;

namespace Data.Model
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
        /// Временной штамп
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
