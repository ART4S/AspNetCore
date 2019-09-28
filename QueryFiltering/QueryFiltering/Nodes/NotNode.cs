using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes
{
    internal class NotNode : BaseNode
    {
        private readonly BaseNode _node;

        public NotNode(BaseNode node)
        {
            _node = node;
        }

        public override Expression BuildExpression()
        {
            return Expression.Not(_node.BuildExpression());
        }
    }
}
