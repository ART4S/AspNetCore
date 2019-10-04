using System;
using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class DateTimeNode : SingleNode
    {
        public DateTimeNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(DateTime.Parse(Value.Replace("datetime","").Trim('\'')));
        }
    }
}
