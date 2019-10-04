using System.Linq.Expressions;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Operators
{
    internal class NotNode : BaseNode
    {
        private readonly BaseNode _node;

        public NotNode(BaseNode node)
        {
            _node = node;
        }

        public override Expression CreateExpression()
        {
            return Expression.Not(_node.CreateExpression());
        }
    }
}
