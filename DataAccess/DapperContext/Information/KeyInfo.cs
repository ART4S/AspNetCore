namespace DapperContext.Information
{
    /// <summary>
    /// Информация о ключе колонки таблицы БД
    /// </summary>
    public class KeyInfo
    {
        /// <summary>
        /// Наименование в БД
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование свойства типа, соответствующего колонке
        /// </summary>
        public string Property { get; set; }
    }
}