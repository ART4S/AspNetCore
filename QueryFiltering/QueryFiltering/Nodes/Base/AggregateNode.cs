namespace QueryFiltering.Nodes.Base
{
    internal abstract class AggregateNode : ExpressionNode
    {
        protected readonly ExpressionNode Left;
        protected readonly ExpressionNode Right;

        protected AggregateNode(ExpressionNode left, ExpressionNode right)
        {
            Left = left;
            Right = right;
        }
    }
}
