using System;

namespace WebFeatures.Domian.Entities.Abstractions
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class BaseEntity<TId> : IUpdatable, IEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
