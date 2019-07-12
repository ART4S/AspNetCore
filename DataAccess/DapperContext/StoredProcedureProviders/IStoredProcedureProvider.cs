namespace DapperContext.StoredProcedureProviders
{
    /// <summary>
    /// Поставщик имен хранимых процедур
    /// </summary>
    public interface IStoredProcedureProvider
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        string GetAll { get; }

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        string GetById { get; }

        /// <summary>
        /// Добавить запись
        /// </summary>
        string Add { get; }

        /// <summary>
        /// Удалить запись
        /// </summary>
        string Remove { get; }
    }
}
