using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class EqualsNode : AggregateNode
    {
        public EqualsNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.Equal(Left.Build(), Right.Build());
        }
    }
}
