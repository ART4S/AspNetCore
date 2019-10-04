using System.Linq.Expressions;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class StringNode : SingleNode
    {
        public StringNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(Value.Trim('\''));
        }
    }
}
