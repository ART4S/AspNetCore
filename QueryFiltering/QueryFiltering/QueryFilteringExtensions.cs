using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using QueryFiltering.Listeners;
using QueryFiltering.Visitors;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("QueryFiltering.Tests")]
namespace QueryFiltering
{
    public static class QueryFilteringExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> sourceQueryable, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return sourceQueryable;
            }

            if (query.StartsWith('?'))
            {
                query = query.Substring(1);
            }

            var queryParams = query.Split('&').Where(x => x.StartsWith('$'));

            var parser = new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(query))));

            foreach (var queryParam in queryParams)
            {
                if (queryParam.StartsWith("$filter"))
                {
                    sourceQueryable = Filter(sourceQueryable, parser);
                    continue;
                }

                if (queryParam.StartsWith("$orderBy"))
                {
                    sourceQueryable = OrderBy(sourceQueryable, parser);
                    continue;
                }
            }

            return sourceQueryable;
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> sourceQueryable, QueryFilteringParser parser)
        {
            var visitor = new FilterVisitor(typeof(T));
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<T, bool>>)tree.Build();

            return sourceQueryable.Where(expression);
        }

        private static IQueryable<T> OrderBy<T>(IQueryable<T> sourceQueryable, QueryFilteringParser parser)
        {
            var walker = new ParseTreeWalker();

            var listener = new OrderByListener(sourceQueryable);
            var tree = parser.orderBy();

            walker.Walk(listener, tree);

            return (IQueryable<T>) listener.OrderedQueryable;
        }
    }
}
