using QueryFiltering.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class QueryVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = Expression.Parameter(_sourceQueryable.ElementType);
        }

        public override IQueryable VisitQuery(QueryFilteringParser.QueryContext context)
        {
            foreach (var parameter in context.queryParameter())
            {
                _sourceQueryable = parameter.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitQueryParameter(QueryFilteringParser.QueryParameterContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitTop(QueryFilteringParser.TopContext context)
        {
            var count = context.Accept(new TopVisitor());

            var take = ReflectionCache.Take
                .MakeGenericMethod(_sourceQueryable.ElementType);

            return (IQueryable) take.Invoke(null, new object[] { _sourceQueryable, count });
        }

        public override IQueryable VisitSkip(QueryFilteringParser.SkipContext context)
        {
            var count = context.Accept(new SkipVisitor());

            var skip = ReflectionCache.Skip
                .MakeGenericMethod(_sourceQueryable.ElementType);

            return (IQueryable) skip.Invoke(null, new object[] { _sourceQueryable, count });
        }

        public override IQueryable VisitFilter(QueryFilteringParser.FilterContext context)
        {
            var visitor = new FilterVisitor(_parameter);
            var tree = context.Accept(visitor);

            var lambda = ReflectionCache.Lambda
                .MakeGenericMethod(typeof(Func<,>)
                    .MakeGenericType(_sourceQueryable.ElementType, typeof(bool)));

            var expression = lambda.Invoke(null, new object[] { tree.BuildExpression(), new ParameterExpression[] { _parameter } });

            var where = ReflectionCache.Where
                .MakeGenericMethod(_sourceQueryable.ElementType);

            return (IQueryable) where.Invoke(null, new[] { _sourceQueryable, expression });
        }

        public override IQueryable VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }
    }
}
