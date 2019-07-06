using DapperContext.Queries.Abstractions;

namespace DapperContext.Queries.Implementations.Sql
{
    internal class SqlSelectQuery: ISelectQuery
    {
        private readonly TableInfo _table;

        public SqlSelectQuery(TableInfo table)
        {
            _table = table;
        }

        public string All()
        {
            return $"SELECT * FROM {_table.Name}";
        }

        public string ById()
        {
            return $"SELECT * FROM {_table.Name}" +
                   $"WHERE {_table.PK}=@{_table.PKProperty}";
        }
    }
}
