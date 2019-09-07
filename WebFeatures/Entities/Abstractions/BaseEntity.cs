namespace Entities.Abstractions
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class BaseEntity<TId> : IEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
