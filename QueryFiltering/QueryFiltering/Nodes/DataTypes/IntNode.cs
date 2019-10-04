using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class IntNode : SingleNode
    {
        public IntNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(int.Parse(Value));
        }
    }
}
