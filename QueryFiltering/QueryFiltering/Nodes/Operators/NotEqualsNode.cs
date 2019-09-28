using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class NotEqualsNode : AggregateNode
    {
        public NotEqualsNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.NotEqual(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
