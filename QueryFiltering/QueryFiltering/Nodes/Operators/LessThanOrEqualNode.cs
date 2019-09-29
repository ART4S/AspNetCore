using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class LessThanOrEqualNode : AggregateNode
    {
        public LessThanOrEqualNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.LessThanOrEqual(Left.Build(), Right.Build());
        }
    }
}
