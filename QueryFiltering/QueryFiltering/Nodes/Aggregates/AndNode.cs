using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Aggregates
{
    internal class AndNode : AggregateNode
    {
        public AndNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.AndAlso(Left.CreateExpression(), Right.CreateExpression());
        }
    }
}
