using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Aggregates
{
    internal class OrNode : AggregateNode
    {
        public OrNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.OrElse(Left.CreateExpression(), Right.CreateExpression());
        }
    }
}
