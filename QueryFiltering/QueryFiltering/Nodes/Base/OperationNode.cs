﻿using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Base
{
    internal abstract class OperationNode : AggregateNode
    {
        protected OperationNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            var left = Left.CreateExpression();
            var right = Expression.Convert(Right.CreateExpression(), left.Type);

            return CreateConcrete(left, right);
        }

        protected abstract Expression CreateConcrete(Expression left, Expression right);
    }
}