using DapperContext.Information;

namespace DapperContext.Queries.Sql
{
    internal class SqlDeleteQuery
    {
        private readonly TableInfo _table;

        public SqlDeleteQuery(TableInfo table)
        {
            _table = table;
        }

        public string DeleteById()
        {
            var query = $"DELETE FROM {_table.Name}" +
                        $"WHERE {_table.PK.Name}=@{_table.PK.Property}";
            
            return query;
        }
    }
}
