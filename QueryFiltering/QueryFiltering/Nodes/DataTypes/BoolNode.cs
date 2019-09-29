﻿using QueryFiltering.Nodes.Base;
using System.Linq.Expressions;

namespace QueryFiltering.Nodes.DataTypes
{
    internal class BoolNode : SingleNode
    {
        public BoolNode(string value) : base(value)
        {
        }

        public override Expression Build()
        {
            return Expression.Constant(bool.Parse(Value));
        }
    }
}
