namespace Entities.Abstractions
{
    /// <summary>
    /// Сущность
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public interface IEntity<TId> where TId : struct
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TId Id { get; set; }
    }
}
