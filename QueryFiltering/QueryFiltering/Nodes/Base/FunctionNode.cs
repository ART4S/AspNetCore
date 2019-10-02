namespace QueryFiltering.Nodes.Base
{
    internal abstract class FunctionNode : BaseNode
    {
        protected BaseNode[] Parameters;

        protected FunctionNode(BaseNode[] parameters)
        {
            Parameters = parameters;
        }
    }
}
