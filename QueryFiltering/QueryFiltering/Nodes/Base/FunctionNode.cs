using System.Collections.Generic;

namespace QueryFiltering.Nodes.Base
{
    internal abstract class FunctionNode : BaseNode
    {
        protected IList<BaseNode> Parameters;

        protected FunctionNode(IList<BaseNode> parameters)
        {
            Parameters = parameters;
        }
    }
}
