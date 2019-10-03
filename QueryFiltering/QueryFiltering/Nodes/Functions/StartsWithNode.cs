using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes.Base;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Functions
{
    internal class StartsWithNode : FunctionNode
    {
        public StartsWithNode(BaseNode[] parameters) : base(parameters)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Call(
                Parameters[0].BuildExpression(),
                ReflectionCache.StartsWith,
                Parameters.Skip(1).Select(x => x.BuildExpression()));
        }
    }
}
