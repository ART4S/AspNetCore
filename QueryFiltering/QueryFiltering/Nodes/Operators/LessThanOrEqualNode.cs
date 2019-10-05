using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class LessThanOrEqualNode : OperationNode
    {
        public LessThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}
