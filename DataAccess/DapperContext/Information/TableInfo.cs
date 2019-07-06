namespace DapperContext.Information
{
    /// <summary>
    /// Информация о таблице БД
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Наименование в БД
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Информация о первичном ключе
        /// </summary>
        public KeyInfo PK { get; set; }

        /// <summary>
        /// Информация о колонках
        /// </summary>
        public ColumnInfo[] Columns { get; set; }
    }
}
