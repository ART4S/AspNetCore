using DapperContext.Queries.Abstractions;

namespace DapperContext.QueryProvider.Abstractions
{
    public interface IQueryProvider
    {
        ISelectQuery Select();

        IInsertQuery Insert();
    }
}
