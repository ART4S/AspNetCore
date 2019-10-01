using QueryFiltering.Nodes.Base;
using System.Globalization;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class FloatNode : SingleNode
    {
        public FloatNode(string value) : base(value)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Constant(float.Parse(Value.Replace("m",""), CultureInfo.InvariantCulture));
        }
    }
}
