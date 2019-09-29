using Antlr4.Runtime.Tree;
using QueryFiltering.Exceptions;
using QueryFiltering.Nodes;
using QueryFiltering.Nodes.Aggregates;
using QueryFiltering.Nodes.Base;
using QueryFiltering.Nodes.DataTypes;
using QueryFiltering.Nodes.Operators;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class FilterVisitor : QueryFilteringBaseVisitor<ExpressionNode>
    {
        private readonly Type _sourceType;
        private readonly ParameterExpression _parameter;

        public FilterVisitor(Type sourceType)
        {
            _sourceType = sourceType;
            _parameter = Expression.Parameter(_sourceType);
        }

        public override ExpressionNode Visit(IParseTree tree)
        {
            return tree.Accept(this);
        }

        public override ExpressionNode VisitTerminal(ITerminalNode node)
        {
            throw new FilterException($"{nameof(VisitTerminal)} -> {node.GetText()}");
        }

        public override ExpressionNode VisitErrorNode(IErrorNode node)
        {
            throw new FilterException($"{nameof(VisitErrorNode)} -> {node.GetText()}");
        }

        public override ExpressionNode VisitFilter(QueryFilteringParser.FilterContext context)
        {
            return new FilterNode(_sourceType, _parameter, context.expression.Accept(this));
        }

        public override ExpressionNode VisitFilterExpression(QueryFilteringParser.FilterExpressionContext context)
        {
            if (context.ChildCount == 1)
            {
                return context.children[0].Accept(this); // VisitAtom
            }

            if (context.ChildCount % 2 == 0)
            {
                throw new FilterException("Ќельз€ применить операцию ветвлени€ - количество дочерних элементов должно быть нечетным");
            }

            ExpressionNode result = null;

            var children = context.children.Reverse().ToArray();

            for (int i = 1; i < children.Length; i += 2)
            {
                var left = result ?? children[i - 1].Accept(this);
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

        public override ExpressionNode VisitAtom(QueryFilteringParser.AtomContext context)
        {
            var result = context.boolExpr?.Accept(this) ?? context.filterExpr?.Accept(this) ?? throw new FilterException($"{nameof(VisitAtom)} -> {context.GetText()}");

            if (context.not != null)
            {
                result = new NotNode(result);
            }

            return result;
        }

        public override ExpressionNode VisitBoolExpression(QueryFilteringParser.BoolExpressionContext context)
        {
            var left = context.left.Accept(this);
            var right = context.right.Accept(this);

            if (context.operation.Type == QueryFilteringLexer.EQUALS)
            {
                return new EqualsNode(left, right);
            }

            if (context.operation.Type == QueryFilteringLexer.NOTEQUALS)
            {
                return new NotEqualsNode(left, right);
            }

            if (context.operation.Type == QueryFilteringLexer.GREATERTHAN)
            {
                return new GreaterThanNode(left, right);
            }

            if (context.operation.Type == QueryFilteringLexer.GREATERTHANOREQUAL)
            {
                return new GreaterThanOrEqualNode(left, right);
            }

            if (context.operation.Type == QueryFilteringLexer.LESSTHAN)
            {
                return new LessThanNode(left, right);
            }

            if (context.operation.Type == QueryFilteringLexer.LESSTHANOREQUAL)
            {
                return new LessThanOrEqualNode(left, right);
            }

            throw new FilterException($"{nameof(VisitBoolExpression)} -> {context.GetText()}");
        }

        public override ExpressionNode VisitProperty(QueryFilteringParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override ExpressionNode VisitConstant(QueryFilteringParser.ConstantContext context)
        {
            if (context.value.Type == QueryFilteringLexer.INT)
            {
                return new IntNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.LONG)
            {
                return new LongNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.DOUBLE)
            {
                return new DoubleNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.FLOAT)
            {
                return new FloatNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.DECIMAL)
            {
                return new DecimalNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.BOOL)
            {
                return new BoolNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.GUID)
            {
                return new GuidNode(context.value.Text);
            }

            if (context.value.Type == QueryFilteringLexer.NULL)
            {
                return new NullNode();
            }

            throw new FilterException($"{nameof(VisitConstant)} -> {context.GetText()}");
        }
    }
}
