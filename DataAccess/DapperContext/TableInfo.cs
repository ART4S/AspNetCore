namespace DapperContext
{
    public class TableInfo
    {
        public string Name { get; }

        public KeyInfo PK { get; }

        public ColumnInfo[] Columns { get; }
    }

    public class ColumnInfo
    {
        public string Name { get; }

        public string Property { get; }
    }

    public class KeyInfo
    {
        public string Name { get; }

        public string Property { get; }
    }
}
