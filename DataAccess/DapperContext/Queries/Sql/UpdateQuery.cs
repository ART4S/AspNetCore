using System.Linq;
using DapperContext.Information;

namespace DapperContext.Queries.Sql
{
    internal class SqlUpdateQuery
    {
        private readonly TableInfo _table;

        public SqlUpdateQuery(TableInfo table)
        {
            _table = table;
        }

        public string Update()
        {
            var qparams = string.Join(',', _table.Columns.Select(x => $"{x.Name}=@{x.Property}"));
            var query = $"UPDATE {_table.Name}" +
                        $"SET {qparams}" +
                        $"WHERE {_table.PK.Name}=@{_table.PK.Property}";

            return query;
        }
    }
}
