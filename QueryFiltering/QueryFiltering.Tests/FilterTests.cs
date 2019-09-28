using QueryFiltering.Tests.Model;
using Xunit;

namespace QueryFiltering.Tests
{
    public class FilterTests
    {
        [Theory]
        [InlineData(21)]
        [InlineData(199)]
        public void ProductPriceLessThan200AndGreaterThan200_ReturnsTrue(int price)
        {
            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterExpressionContext>(
                "Price lt 200 and Price gt 20", 
                parser => parser.filterExpression()).Compile();

            var product = new Product
            {
                Price = price
            };

            Assert.True(func(product));
        }

        [Theory]
        [InlineData(19)]
        [InlineData(20)]
        [InlineData(200)]
        [InlineData(201)]
        public void product_price_lt_200_and_price_gt_20_return_false(int price)
        {
            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterExpressionContext>(
                "Price lt 200 and Price gt 20",
                parser => parser.filterExpression()).Compile();

            var product = new Product
            {
                Price = price
            };

            Assert.False(func(product));
        }

        [Theory]
        [InlineData(150, 2)]
        [InlineData(35, 2.05)]
        public void product_price_lt_200_and_Price_gt_35_or_price_eq_35_and_discount_eq_2_05_ReturnsTrue(int price, double discount)
        {
            var product = new Product
            {
                Price = price,
                Discount = discount
            };

            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterExpressionContext>(
                "(Price lt 200 and Price gt 35) or (Price eq 35 and Discount eq 2.05)",
                parser => parser.filterExpression()).Compile();

            Assert.True(func(product));
        }

        [Theory]
        [InlineData(35, 2)]
        [InlineData(35, 2.06)]
        public void product_price_lt_200_and_Price_gt_35_or_price_eq_35_and_discount_eq_2_05_ReturnsFalse(int price, double discount)
        {
            var product = new Product
            {
                Price = price,
                Discount = discount
            };

            var func = LambdaFactory.Create<Product, bool, QueryFilteringParser.FilterExpressionContext>(
                "(Price lt 200 and Price gt 35) or (Price eq 35 and Discount eq 2.05)",
                parser => parser.filterExpression()).Compile();

            Assert.False(func(product));
        }
    }
}
