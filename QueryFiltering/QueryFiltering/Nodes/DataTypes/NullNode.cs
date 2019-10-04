using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class NullNode : BaseNode
    {
        public override Expression CreateExpression()
        {
            return Expression.Constant(null);
        }
    }
}
