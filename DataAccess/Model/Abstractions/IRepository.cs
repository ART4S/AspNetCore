using Model.Entities.Base;
using System.Collections.Generic;

namespace Model.Abstractions
{
    /// <summary>
    /// Репозиторий доступа к данным
    /// </summary>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить список всех сущностей
        /// </summary>
        List<TEntity> GetAll();

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Id сущности</param>
        TEntity GetById(int id);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Add(TEntity entity);

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        /// <param name="id">Id сущности</param>
        void Remove(int id);
    }
}
