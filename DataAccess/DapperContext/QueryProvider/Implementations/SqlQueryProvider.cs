using DapperContext.Queries.Abstractions;
using DapperContext.Queries.Implementations.Sql;
using DapperContext.QueryProvider.Abstractions;

namespace DapperContext.QueryProvider.Implementations
{
    public class SqlQueryProvider : IQueryProvider
    {
        private readonly TableInfo _tableInfo;

        public SqlQueryProvider(TableInfo tableInfo)
        {
            _tableInfo = tableInfo;
        }

        public ISelectQuery Select()
        {
            return new SqlSelectQuery(_tableInfo);
        }

        public IInsertQuery Insert()
        {
            return new SqlInsertQuery(_tableInfo);
        }
    }
}
