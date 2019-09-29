using System.Linq.Expressions;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Operators
{
    internal class NotNode : ExpressionNode
    {
        private readonly ExpressionNode _node;

        public NotNode(ExpressionNode node)
        {
            _node = node;
        }

        public override Expression Build()
        {
            return Expression.Not(_node.Build());
        }
    }
}
