using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class EqualsNode : AggregateNode
    {
        public EqualsNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Equal(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
