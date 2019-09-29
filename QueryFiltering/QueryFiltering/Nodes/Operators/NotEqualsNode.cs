using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class NotEqualsNode : AggregateNode
    {
        public NotEqualsNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.NotEqual(Left.Build(), Right.Build());
        }
    }
}
