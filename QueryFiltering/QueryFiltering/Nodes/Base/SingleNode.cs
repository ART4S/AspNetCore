namespace QueryFiltering.Nodes.Base
{
    internal abstract class SingleNode : ExpressionNode
    {
        protected readonly string Value;

        protected SingleNode(string value)
        {
            Value = value;
        }
    }
}
