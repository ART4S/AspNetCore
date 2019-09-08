using System;

namespace WebFeatures.Entities.Abstractions
{
    /// <summary>
    /// Обновляемая сущность
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата редактирования
        /// </summary>
        DateTime? UpdatedAt { get; set; }
    }
}
