using DapperContext.Information;

namespace DapperContext.Queries.Sql
{
    internal class SqlSelectQuery
    {
        private readonly TableInfo _table;

        public SqlSelectQuery(TableInfo table)
        {
            _table = table;
        }

        public string All()
        {
            var query = $"SELECT * FROM {_table.Name}";
            return query;
        }

        public string ById()
        {
            var query = $"SELECT * FROM {_table.Name} " +
                        $"WHERE {_table.PK.Name}=@{_table.PK.Property}";

            return query;
        }
    }
}
