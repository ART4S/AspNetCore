using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class NullNode : BaseNode
    {
        public override Expression BuildExpression()
        {
            return Expression.Constant(null);
        }
    }
}
