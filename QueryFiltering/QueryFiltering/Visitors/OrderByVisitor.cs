using QueryFiltering.Infrastructure;
using System;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class OrderByVisitor : QueryFilteringBaseVisitor<object>
    {
        private object _sourceQueryable;
        private readonly Type _elementType;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(object sourceQueryable, Type elementType, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _elementType = elementType;
            _parameter = parameter;
        }

        public override object VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override object VisitOrderByExpression(QueryFilteringParser.OrderByExpressionContext context)
        {
            foreach (var orderProperty in context.orderByProperty())
            {
                _sourceQueryable = orderProperty.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitOrderByProperty(QueryFilteringParser.OrderByPropertyContext context)
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
                typeof(Func<,>).MakeGenericType(_elementType, property.Type));

            var expression = lambda.Invoke(null, new object[] { property, new ParameterExpression[] { _parameter } });
            
            var orderBy = (context.op.Type == QueryFilteringLexer.ASC ? 
                    context.firstSort ? ReflectionCache.OrderBy : ReflectionCache.ThenBy : 
                    context.firstSort ? ReflectionCache.OrderByDescending : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_elementType, property.Type);

            return orderBy.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
