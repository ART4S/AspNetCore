using System.Linq;
using WebFeatures.QueryFiltering.Filters;

namespace WebFeatures.QueryFiltering.Extensions
{
    public static class QueryFilterExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            return sourceQueryable;
        }
    }
}