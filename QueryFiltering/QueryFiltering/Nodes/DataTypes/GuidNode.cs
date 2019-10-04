using QueryFiltering.Nodes.Base;
using System;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class GuidNode : SingleNode
    {
        public GuidNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(Guid.Parse(Value));
        }
    }
}
