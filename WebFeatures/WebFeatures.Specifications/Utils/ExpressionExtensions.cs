using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Utils
{
    internal static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Merge(right, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            => left.Merge(right, Expression.OrElse);

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
            => Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);

        public static Expression<TFunc> Merge<TFunc>(
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

        public static Expression<Func<T, bool>> Combine<T, TProp>(
            this Expression<Func<T, TProp>> left,
            Expression<Func<TProp, bool>> right)
        {
            var body = new ExpressionReplacerVisitor(right.Parameters[0], left.Body).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(body, left.Parameters[0]);
        }

        #region Visitors

        private class ExpressionReplacerVisitor : ExpressionVisitor
        {
            private readonly Expression _source;
            private readonly Expression _replacement;

            public ExpressionReplacerVisitor(Expression source, Expression replacement)
            {
                _source = source;
                _replacement = replacement;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _source)
                {
                    node = _replacement;
                }

                return base.Visit(node);
            }
        }

        private class ParamsReplacerVisitor : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _replacementsMap;

            public ParamsReplacerVisitor(Dictionary<ParameterExpression, ParameterExpression> replacementsMap)
            {
                _replacementsMap = replacementsMap;
            }

            protected override Expression VisitParameter(ParameterExpression param)
            {
                if (_replacementsMap.TryGetValue(param, out var replacement))
                {
                    param = replacement;
                }

                return base.VisitParameter(param);
            }
        }

        #endregion
    }
}
