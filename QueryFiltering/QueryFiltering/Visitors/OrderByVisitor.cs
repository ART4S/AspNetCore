using QueryFiltering.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class OrderByVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override IQueryable VisitOrderByExpression(QueryFilteringParser.OrderByExpressionContext context)
        {
            foreach (var orderProperty in context.orderByProperty())
            {
                _sourceQueryable = orderProperty.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitOrderByProperty(QueryFilteringParser.OrderByPropertyContext context)
        {
            MemberExpression property = null;

            foreach (var propName in context.value.Text.Split("."))
            {
                if (property == null)
                {
                    property = Expression.Property(_parameter, propName);
                }
                else
                {
                    property = Expression.Property(property, propName);
                }
            }

            var lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_sourceQueryable.ElementType, property.Type));

            var expression = lambda.Invoke(null, new object[] { property, new ParameterExpression[] { _parameter } });
            
            var orderBy = (context.op.Type == QueryFilteringLexer.ASC ? 
                    context.firstSort ? ReflectionCache.OrderBy : ReflectionCache.ThenBy : 
                    context.firstSort ? ReflectionCache.OrderByDescending : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_sourceQueryable.ElementType, property.Type);

            return (IQueryable)orderBy.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
