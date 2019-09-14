﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Where<T, TProp>(this IQueryable<T> sourceQueryable,
            Expression<Func<T, TProp>> propGetter,
            Expression<Func<TProp, bool>> propCondition)
        {
            var expr = propGetter.Compose(propCondition);
            return sourceQueryable.Where(expr);
        }
    }
}
