using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes.Base;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Functions
{
    internal class EndsWithNode : FunctionNode
    {
        public EndsWithNode(BaseNode[] parameters) : base(parameters)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Call(
                Parameters[0].CreateExpression(),
                ReflectionCache.EndsWith,
                Parameters.Skip(1).Select(x => x.CreateExpression()));
        }
    }
}
