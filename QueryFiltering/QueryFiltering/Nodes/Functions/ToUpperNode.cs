using System.Collections.Generic;
using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes.Base;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.Functions
{
    internal class ToUpperNode : FunctionNode
    {
        public ToUpperNode(IList<BaseNode> parameters) : base(parameters)
        {
        }

        public override Expression BuildExpression()
        {
            return Expression.Call(
                Parameters[0].BuildExpression(), 
                ReflectionCache.ToUpper, 
                Parameters.Skip(0).Select(x => x.BuildExpression()));
        }
    }
}
