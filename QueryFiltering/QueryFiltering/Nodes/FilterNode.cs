using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes.Base;
using System;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes
{
    internal class FilterNode : ExpressionNode
    {
        private readonly Type _type;
        private readonly ParameterExpression _parameter;
        private readonly ExpressionNode _node;

        public FilterNode(Type type, ParameterExpression parameter, ExpressionNode node)
        {
            _type = type;
            _parameter = parameter;
            _node = node;
        }

        public override Expression Build()
        {
            var lambda = ReflectionCache.Lambda
                .MakeGenericMethod(typeof(Func<,>)
                    .MakeGenericType(_type, typeof(bool)));

            var expression = lambda.Invoke(null, new object[] { _node.Build(), new ParameterExpression[] {_parameter} });

            return (Expression) expression;
        }
    }
}
