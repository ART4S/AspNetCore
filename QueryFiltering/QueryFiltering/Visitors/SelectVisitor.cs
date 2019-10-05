using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class SelectVisitor : QueryFilteringBaseVisitor<object>
    {
        private readonly object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SelectVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitSelect(QueryFilteringParser.SelectContext context)
        {
            return context.expression.Accept(this);
        }

        public override object VisitSelectExpression(QueryFilteringParser.SelectExpressionContext context)
        {
            var properties = context.PROPERTYACCESS().Select(x => x.Symbol.Text).ToHashSet();
            var anonymousType = ReflectionUtils.CreateAnonymousTypeFromSourceType(_parameter.Type, properties);

            var method = ReflectionCache.Select.MakeGenericMethod(_parameter.Type, anonymousType);

            var body = Expression.New(
                anonymousType.GetConstructors().Single(), 
                properties.Select(x => new PropertyNode(x, _parameter).CreateExpression()));

            var lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, anonymousType));

            var expression = lambda.Invoke(null, new object[] {body, new ParameterExpression[] {_parameter}});

            return method.Invoke(null, new [] {_sourceQueryable, expression });
        }
    }
}
