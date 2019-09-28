using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class IntNode : SingleNode
    {
        public IntNode(string value) : base(value)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Constant(int.Parse(Value));
        }
    }
}
