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

        public override Expression CreateExpression()
        {
            return Expression.Constant(decimal.Parse(Value.Replace("m",""), CultureInfo.InvariantCulture));
        }
    }
}
