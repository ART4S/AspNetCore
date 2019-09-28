using System.Linq;

namespace QueryFiltering
{
    public static class QueryFilteringExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> sourceQueryable, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return sourceQueryable;
            }

            if (!filter.StartsWith('?'))
            {
                return sourceQueryable;
            }

            var queryParams = filter.Substring(1).Split('&').Where(x => x.StartsWith("$"));

            foreach (var queryParam in queryParams)
            {
                if (queryParam.StartsWith("$filter"))
                {
                }

                if (queryParam.StartsWith("$orderBy"))
                {
                }
            }

            return sourceQueryable;
        }
    }
}
