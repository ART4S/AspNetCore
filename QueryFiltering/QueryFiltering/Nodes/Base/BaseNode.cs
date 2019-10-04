using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Base
{
    internal abstract class BaseNode
    {
        public abstract Expression CreateExpression();
    }
}
