using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class NullNode : ExpressionNode
    {
        public override Expression Build()
        {
            return Expression.Constant(null);
        }
    }
}
