using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes
{
    internal class OrderByNode : BaseNode<List<LambdaExpression>>
    {
        private readonly Type _sourceType;
        private readonly ParameterExpression _parameter;
        private readonly (string property, string sortType)[] _sortParams;

        public OrderByNode(Type sourceType, ParameterExpression parameter, (string property, string sortType)[] sortParams)
        {
            _sourceType = sourceType;
            _parameter = parameter;
            _sortParams = sortParams;
        }

        public override List<LambdaExpression> Build()
        {
            var result = new List<LambdaExpression>();

            foreach (var param in _sortParams)
            {
                MemberExpression memberAccess = null;
                Type lastPropertyType = null;

                foreach (var propName in param.property.Split("."))
                {
                    if (memberAccess == null)
                    {
                        memberAccess = Expression.Property(_parameter, propName);
                    }
                    else
                    {
                        memberAccess = Expression.Property(memberAccess, propName);
                    }

                    lastPropertyType = memberAccess.Expression.Type;
                }

                var lambda = ReflectionCache.Lambda
                    .MakeGenericMethod(typeof(Func<,>).MakeGenericType(_sourceType, lastPropertyType));

                var expression = (LambdaExpression) lambda.Invoke(
                    null, 
                    new object[] { memberAccess, new ParameterExpression[] { _parameter } });

                result.Add(expression);
            }

            return result;
        }
    }
}
