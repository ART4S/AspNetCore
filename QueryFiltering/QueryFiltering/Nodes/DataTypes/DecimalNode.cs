using QueryFiltering.Nodes.Base;
using System.Globalization;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class DecimalNode : SingleNode
    {
        public DecimalNode(string value) : base(value)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Constant(decimal.Parse(Value, CultureInfo.InvariantCulture));
        }
    }
}
