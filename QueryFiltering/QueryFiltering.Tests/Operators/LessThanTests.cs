using QueryFiltering.Tests.Model;
using Xunit;

namespace QueryFiltering.Tests.Operators
{
    public class LessThanTests
    {
        private readonly Product _product = new Product();

        [Fact]
        public void product_price_lt_200_returns_true()
        {
            _product.Price = 190;


        }

        [Fact]
        public void product_price_lt_200_returns_false()
        {

        }
    }
}
