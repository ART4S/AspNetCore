using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes
{
    internal class PropertyNode : SingleNode
    {
        private readonly ParameterExpression _parameter;

        public PropertyNode(string value, ParameterExpression parameter) : base(value)
        {
            _parameter = parameter;
        }

        public override Expression BuildExpression()
        {
            MemberExpression property = null;

            foreach (var propertyName in Value.Split("/"))
            {
                if (property == null)
                {
                    property = Expression.Property(_parameter, propertyName);
                }
                else
                {
                    property = Expression.Property(property, propertyName);
                }
            }

            return property;
        }
    }
}
