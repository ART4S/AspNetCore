using LinqToQuerystring;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Results;

namespace WebFeatures.QueryFiltering.Utils
{
    public static class QueryableExtensions
    {
        public static FilteringResult ApplyFilter<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            try
            {
                var queryString = filter.BuildQueryString();
                var records = sourceQueryable.LinqToQuerystring(queryString).ToList();

                return new FilteringResult(records);
            }
            catch
            {
                throw new FilteringException("Ошибка во время фильтрации результата");
            }
        }

        private static string BuildQueryString(this QueryFilter filter)
        {
            var filteringParams = new List<string>();

            if (filter.Skip.HasValue && filter.Skip.Value > 0)
            {
                filteringParams.Add($"$skip={filter.Skip.Value}");
            }

            if (filter.Top.HasValue && filter.Top.Value > 0)
            {
                filteringParams.Add($"$top={filter.Top.Value}");
            }

            if (filter.Filter != null)
            {
                filteringParams.Add($"$filter={filter.Filter}");
            }

            if (filter.OrderBy != null)
            {
                filteringParams.Add($"$orderby={filter.OrderBy}");
            }

            return string.Join(",", filteringParams);
        }

        // TODO: в разработке. Буду избавляться от LinqToQuerystring
        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            var prop = typeof(T).GetProperties()
                .FirstOrDefault(x => x.Name == filter.OrderBy);

            if (prop == null)
            {
                throw new ArgumentException();
            }

            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.Property(parameter, prop);

            var lambda = _lambda.GetOrAdd(prop.PropertyType, propType =>
            {
                return typeof(Expression)
                    .GetMethods()
                    .First(x => x.Name == "Lambda")
                    .MakeGenericMethod(typeof(Func<,>).MakeGenericType(typeof(T), prop.PropertyType));
            });

            var orderingExpression = lambda.Invoke(null, new object[] {body, new ParameterExpression[] {parameter}});

            MethodInfo orderBy;
            
            if (filter.OrderBy.Contains("desc"))
            {
                orderBy = _orderByDesc.GetOrAdd(prop.PropertyType, propType =>
                {
                    return typeof(Queryable)
                        .GetMethods()
                        .First(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), propType);
                });
            }
            else
            {
                orderBy = _orderBy.GetOrAdd(prop.PropertyType, propType =>
                {
                    return typeof(Queryable)
                        .GetMethods()
                        .First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), propType);
                });
            }

            return (IOrderedQueryable<T>) orderBy.Invoke(null, new[] { sourceQueryable, orderingExpression });
        }

        private static readonly ConcurrentDictionary<Type, MethodInfo> _lambda = new ConcurrentDictionary<Type, MethodInfo>();

        private static readonly ConcurrentDictionary<Type, MethodInfo> _orderBy = new ConcurrentDictionary<Type, MethodInfo>();

        private static readonly ConcurrentDictionary<Type, MethodInfo> _orderByDesc = new ConcurrentDictionary<Type, MethodInfo>();
    }
}