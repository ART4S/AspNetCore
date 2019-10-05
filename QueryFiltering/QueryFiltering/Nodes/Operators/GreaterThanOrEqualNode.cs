using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class GreaterThanOrEqualNode : OperationNode
    {
        public GreaterThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}
