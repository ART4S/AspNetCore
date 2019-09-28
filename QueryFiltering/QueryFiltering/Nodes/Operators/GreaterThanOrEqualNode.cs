using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanOrEqualNode : AggregateNode
    {
        public GreaterThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.GreaterThanOrEqual(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
