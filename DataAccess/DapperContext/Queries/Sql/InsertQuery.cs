using DapperContext.Information;
using System.Linq;

namespace DapperContext.Queries.Sql
{
    internal class SqlInsertQuery
    {
        private readonly TableInfo _table;

        public SqlInsertQuery(TableInfo table)
        {
            _table = table;
        }

        public string Insert()
        {
            var qparams = string.Join(',', _table.Columns.Select(x => x.Name));
            var qvalues = string.Join(',', _table.Columns.Select(x => "@" + x.Property));

            var query = $"INSERT INTO {_table.Name} ({qparams}) " +
                        $"VALUES ({qvalues});";

            return query;
        }

        public string InsertAndReturnId()
        {
            var query = Insert() + "SELECT CAST(SCOPE_IDENTITY() as int);";
            return query;
        }
    }
}
