using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanOrEqualNode : AggregateNode
    {
        public GreaterThanOrEqualNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.GreaterThanOrEqual(Left.Build(), Right.Build());
        }
    }
}
