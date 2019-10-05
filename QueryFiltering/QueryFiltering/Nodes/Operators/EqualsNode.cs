using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Operators
{
    internal class EqualsNode : OperationNode
    {
        public EqualsNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }
    }
}
