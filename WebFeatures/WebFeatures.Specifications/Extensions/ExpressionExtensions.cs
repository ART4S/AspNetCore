using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Compose(right, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Compose(right, Expression.OrElse);

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
            => Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);

        public static Expression<TFunc> Compose<TFunc>(
            this Expression<TFunc> left,
            Expression<TFunc> right,
            Func<Expression, Expression, Expression> merge)
        {
            var map = left.Parameters
                .Select((param, index) => new { r = right.Parameters[index], l = param })
                .ToDictionary(x => x.r, x => x.l);

            var rightBody = new ParamsReplacerVisitor(map).Visit(right.Body);

            return Expression.Lambda<TFunc>(merge(left.Body, rightBody), left.Parameters);
        }

        private class ParamsReplacerVisitor : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            public ParamsReplacerVisitor(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map;
            }

            protected override Expression VisitParameter(ParameterExpression param)
            {
                if (_map.TryGetValue(param, out var replacement))
                {
                    param = replacement;
                }

                return base.VisitParameter(param);
            }
        }
    }
}
