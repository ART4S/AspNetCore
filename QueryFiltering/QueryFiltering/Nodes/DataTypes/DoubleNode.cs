using QueryFiltering.Nodes.Base;
using System.Globalization;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class DoubleNode : SingleNode
    {
        public DoubleNode(string value) : base(value)
        {
        }

        public override Expression Build()
        {
            return Expression.Constant(double.Parse(Value.Replace("d",""), CultureInfo.InvariantCulture));
        }
    }
}
