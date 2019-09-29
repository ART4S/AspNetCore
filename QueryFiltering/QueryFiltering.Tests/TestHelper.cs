using Antlr4.Runtime;

namespace QueryFiltering.Tests
{
    internal static class TestHelper
    {
        public static QueryFilteringParser CreateParser(string input)
        {
            return new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(input))));
        }
    }
}
