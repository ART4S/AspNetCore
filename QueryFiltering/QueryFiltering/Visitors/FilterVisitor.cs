using Antlr4.Runtime.Tree;
using QueryFiltering.Exceptions;
using QueryFiltering.Nodes;
using QueryFiltering.Nodes.Aggregates;
using QueryFiltering.Nodes.Base;
using QueryFiltering.Nodes.DataTypes;
using QueryFiltering.Nodes.Operators;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class FilterVisitor : QueryFilteringBaseVisitor<BaseNode>
    {
        private readonly ParameterExpression _parameter;

        public FilterVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        public override BaseNode Visit(IParseTree tree)
        {
            return tree.Accept(this);
        }

        public override BaseNode VisitFilter(QueryFilteringParser.FilterContext context)
        {
            return context.expression.Accept(this);
        }

        public override BaseNode VisitFilterExpression(QueryFilteringParser.FilterExpressionContext context)
        {
            var children = context.children.Reverse().ToArray();

            BaseNode result = children[0].Accept(this);

            for (int i = 1; i < children.Length; i += 2)
            {
                var left = result;
                var right = children[i + 1].Accept(this);

                var node = (ITerminalNode)children[i];

                if (node.Symbol.Type == QueryFilteringLexer.AND)
                {
                    result = new AndNode(left, right);
                    continue;
                }

                if (node.Symbol.Type == QueryFilteringLexer.OR)
                {
                    result = new OrNode(left, right);
                    continue;
                }

                throw new FilterException($"{nameof(VisitFilterExpression)} -> {node.GetText()}");
            }

            return result;
        }

        public override BaseNode VisitAtom(QueryFilteringParser.AtomContext context)
        {
            var result = context.boolExpr?.Accept(this) ?? context.filterExpr?.Accept(this) ?? throw new FilterException($"{nameof(VisitAtom)} -> {context.GetText()}");

            if (context.not != null)
            {
                result = new NotNode(result);
            }

            return result;
        }

        public override BaseNode VisitBoolExpression(QueryFilteringParser.BoolExpressionContext context)
        {
            var left = context.left.Accept(this);
            var right = context.right.Accept(this);

            switch (context.operation.Type)
            {
                case QueryFilteringLexer.EQUALS:
                    return new EqualsNode(left, right);
                case QueryFilteringLexer.NOTEQUALS:
                    return new NotEqualsNode(left, right);
                case QueryFilteringLexer.GREATERTHAN:
                    return new GreaterThanNode(left, right);
                case QueryFilteringLexer.GREATERTHANOREQUAL:
                    return new GreaterThanOrEqualNode(left, right);
                case QueryFilteringLexer.LESSTHAN:
                    return new LessThanNode(left, right);
                case QueryFilteringLexer.LESSTHANOREQUAL:
                    return new LessThanOrEqualNode(left, right);
                default:
                    throw new FilterException($"{nameof(VisitBoolExpression)} -> {context.GetText()}");
            }
        }

        public override BaseNode VisitProperty(QueryFilteringParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override BaseNode VisitConstant(QueryFilteringParser.ConstantContext context)
        {
            switch (context.value.Type)
            {
                case QueryFilteringLexer.INT:
                    return new IntNode(context.value.Text);
                case QueryFilteringLexer.LONG:
                    return new LongNode(context.value.Text);
                case QueryFilteringLexer.DOUBLE:
                    return new DoubleNode(context.value.Text);
                case QueryFilteringLexer.FLOAT:
                    return new FloatNode(context.value.Text);
                case QueryFilteringLexer.DECIMAL:
                    return new DecimalNode(context.value.Text);
                case QueryFilteringLexer.BOOL:
                    return new BoolNode(context.value.Text);
                case QueryFilteringLexer.GUID:
                    return new GuidNode(context.value.Text);
                case QueryFilteringLexer.NULL:
                    return new NullNode();
                default:
                    throw new FilterException($"{nameof(VisitConstant)} -> {context.GetText()}");
            }
        }
    }
}
