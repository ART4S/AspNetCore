using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanNode : OperationNode
    {
        public GreaterThanNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
    }
}
