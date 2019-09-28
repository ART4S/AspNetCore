using Antlr4.Runtime;
using System;
using System.Linq.Expressions;

namespace QueryFiltering.Tests
{
    internal static class LambdaFactory
    {
        public static Expression<Func<TIn, TOut>> Create<TIn, TOut, TContext>(string input, Func<QueryFilteringParser, TContext> contextProvider) 
            where TContext : ParserRuleContext
        {
            var parser = new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(input))));

            var ctx = contextProvider(parser);
            var parameter = Expression.Parameter(typeof(TIn), "x");

            var tree = ctx.Accept(new FilterVisitor(parameter));
            return Expression.Lambda<Func<TIn, TOut>>(tree.BuildExpression(), parameter);
        }
    }
}
