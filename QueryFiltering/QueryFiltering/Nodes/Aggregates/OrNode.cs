using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Aggregates
{
    internal class OrNode : AggregateNode
    {
        public OrNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.OrElse(Left.BuildExpression(), Right.BuildExpression());
        }
    }
}
