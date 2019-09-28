using QueryFiltering.Tests.Model;
using Xunit;

namespace QueryFiltering.Tests.Operators
{
    public class EqualsTests
    {
        [Fact]
        public void product_price_eq_200_return_true()
        {
            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterContext>(
                "Price eq 200",
                parser => parser.filter()).Compile();

            var product = new Product
            {
                Price = 200
            };

            Assert.True(func(product));
        }

        [Fact]
        public void product_price_eq_200_return_false()
        {
            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterContext>(
                "Price eq 200",
                parser => parser.filter()).Compile();

            var product = new Product
            {
                Price = -1
            };

            Assert.False(func(product));
        }
    }
}
