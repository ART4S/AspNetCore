using DapperContext.Information;

namespace DapperContext.Queries.Sql.Customers
{
    internal class SelectCustomersWithDependenciesQuery
    {
        private readonly TableInfo _table;

        public SelectCustomersWithDependenciesQuery(TableInfo table)
        {
            _table = table;
        }

        public string Body()
        {
            return string.Empty;
        }
    }
}
