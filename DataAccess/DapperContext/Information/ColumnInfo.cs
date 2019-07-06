namespace DapperContext.Information
{
    /// <summary>
    /// Информация о колонке таблицы БД
    /// </summary>
    public class ColumnInfo
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