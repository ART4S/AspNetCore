using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class LongNode : SingleNode
    {
        public LongNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(long.Parse(Value.Replace("l","")));
        }
    }
}
