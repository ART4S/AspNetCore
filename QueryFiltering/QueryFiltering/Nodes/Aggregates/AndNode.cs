using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Aggregates
{
    internal class AndNode : AggregateNode
    {
        public AndNode(ExpressionNode left, ExpressionNode right) : base(left, right)
        {
        }

        public override Expression Build()
        {
            return Expression.AndAlso(Left.Build(), Right.Build());
        }
    }
}
