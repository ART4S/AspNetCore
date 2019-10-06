using Antlr4.Runtime;
using QueryFiltering.Visitors;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("QueryFiltering.Tests")]
namespace QueryFiltering
{
    public static class QueryFilteringExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> sourceQueryable, string query)
        {
            if (sourceQueryable == null) throw new ArgumentNullException(nameof(sourceQueryable));
            if (query == null) throw new ArgumentNullException(nameof(query));

            return (IQueryable<T>) ApplyQueryImpl(sourceQueryable, query);
        }

        public static IQueryable<dynamic> ApplyQueryAsDynamic<T>(this IQueryable<T> sourceQueryable, string query)
        {
            if (sourceQueryable == null) throw new ArgumentNullException(nameof(sourceQueryable));
            if (query == null) throw new ArgumentNullException(nameof(query));

            return (IQueryable<dynamic>) ApplyQueryImpl(sourceQueryable, query);
        }

        private static object ApplyQueryImpl(IQueryable sourceQueryable, string query)
        {
            var parser = new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(query))));

            var visitor = new QueryVisitor(sourceQueryable);
            var resultQueryable = parser.query().Accept(visitor);

            return resultQueryable;
        }
    }
}
