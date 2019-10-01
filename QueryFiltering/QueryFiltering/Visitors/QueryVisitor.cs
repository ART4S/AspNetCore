using QueryFiltering.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class QueryVisitor : QueryFilteringBaseVisitor<object>
    {
        private object _sourceQueryable;
        private readonly Type _elementType;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _elementType = sourceQueryable.ElementType;
            _parameter = Expression.Parameter(sourceQueryable.ElementType);
        }

        public override object VisitQuery(QueryFilteringParser.QueryContext context)
        {
            foreach (var parameter in context.queryParameter())
            {
                _sourceQueryable = parameter.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitQueryParameter(QueryFilteringParser.QueryParameterContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitTop(QueryFilteringParser.TopContext context)
        {
            var count = context.Accept(new TopVisitor());

            var take = ReflectionCache.Take
                .MakeGenericMethod(_elementType);

            return take.Invoke(null, new[] { _sourceQueryable, count });
        }

        public override object VisitSkip(QueryFilteringParser.SkipContext context)
        {
            var count = context.Accept(new SkipVisitor());

            var skip = ReflectionCache.Skip
                .MakeGenericMethod(_elementType);

            return skip.Invoke(null, new[] { _sourceQueryable, count });
        }

        public override object VisitFilter(QueryFilteringParser.FilterContext context)
        {
            var visitor = new FilterVisitor(_parameter);
            var tree = context.Accept(visitor);

            var lambda = ReflectionCache.Lambda
                .MakeGenericMethod(typeof(Func<,>)
                    .MakeGenericType(_elementType, typeof(bool)));

            var expression = lambda.Invoke(null, new object[] { tree.BuildExpression(), new ParameterExpression[] { _parameter } });

            var where = ReflectionCache.Where
                .MakeGenericMethod(_elementType);

            return where.Invoke(null, new[] { _sourceQueryable, expression });
        }

        public override object VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _elementType, _parameter);
            return context.Accept(visitor);
        }
    }
}
