using System.Linq;

namespace WebFeatures.WebApi.QueryFilters
{
    // TODO: запилить фильтрацию
    public class QueryFilter
    {

    }

    static class QueryFilterExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            return sourceQueryable;
        }
    }
}
