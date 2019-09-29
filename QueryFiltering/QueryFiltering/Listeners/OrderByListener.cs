using QueryFiltering.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Listeners
{
    internal class OrderByListener : QueryFilteringBaseListener
    {
        public IQueryable OrderedQueryable => _orderedQueryable;
        private IQueryable _orderedQueryable;

        private readonly ParameterExpression _parameter;

        private bool _ordered = false;

        public OrderByListener(IQueryable sourceQueryable)
        {
            _orderedQueryable = sourceQueryable;
            _parameter = Expression.Parameter(_orderedQueryable.ElementType);
        }

        public override void ExitOrderProperty(QueryFilteringParser.OrderPropertyContext context)
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
                    typeof(Func<,>).MakeGenericType(_orderedQueryable.ElementType, property.Type));

            var expression = lambda.Invoke(null, new object[] {property, new ParameterExpression[] {_parameter}});

            var orderBy = (context.op.Type == QueryFilteringLexer.ASC 
                    ? !_ordered ? ReflectionCache.OrderBy : ReflectionCache.ThenBy 
                    : !_ordered ? ReflectionCache.OrderByDescending : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_orderedQueryable.ElementType, property.Type);

            _orderedQueryable = (IQueryable) orderBy.Invoke(null, new[] { _orderedQueryable, expression });
        }
    }
}
