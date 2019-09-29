using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanNode : AggregateNode
    {
        public GreaterThanNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.GreaterThan(Left.Build(), Right.Build());
        }
    }
}
