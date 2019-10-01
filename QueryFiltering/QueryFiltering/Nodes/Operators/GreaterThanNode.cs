using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanNode : AggregateNode
    {
        public GreaterThanNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.GreaterThan(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
