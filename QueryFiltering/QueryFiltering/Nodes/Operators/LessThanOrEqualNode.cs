using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class LessThanOrEqualNode : AggregateNode
    {
        public LessThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.LessThanOrEqual(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
