namespace DapperContext.QueryProviders.Abstractions
{
    /// <summary>
    /// Поставщик запросов
    /// </summary>
    public interface IQueryProvider
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        string GetAllQuery { get; }

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        string GetByIdQuery { get; }

        /// <summary>
        /// Добавить запись
        /// </summary>
        string AddQuery { get; }

        /// <summary>
        /// Удалить запись
        /// </summary>
        string RemoveQuery { get; }
    }
}
