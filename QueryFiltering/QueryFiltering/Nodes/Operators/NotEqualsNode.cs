using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class NotEqualsNode : OperationNode
    {
        public NotEqualsNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.NotEqual(left, right);
        }
    }
}
