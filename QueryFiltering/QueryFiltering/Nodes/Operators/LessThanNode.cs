using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class LessThanNode : AggregateNode
    {
        public LessThanNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.LessThan(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
