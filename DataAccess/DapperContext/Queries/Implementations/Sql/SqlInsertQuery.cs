using DapperContext.Queries.Abstractions;
using System.Linq;

namespace DapperContext.Queries.Implementations.Sql
{
    public class SqlInsertQuery : IInsertQuery
    {
        private readonly TableInfo _table;

        public SqlInsertQuery(TableInfo table)
        {
            _table = table;
        }

        public string Body()
        {
            var qparams = string.Join(',', _table.Columns.Select(x => "@" + x.Property));
            var qvalues = string.Join(',', _table.Columns.Select(x => x.Name));

            return $"INSERT INTO {_table.Name} ({qparams}) " +
                   $"VALUES ({qvalues})";
        }

        public string ReturnId()
        {
            var sql = Body() + ;
        }
    }
}
