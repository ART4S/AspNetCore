using QueryFiltering.Tests.Model;
using System.Linq;
using Xunit;

namespace QueryFiltering.Tests
{
    public class QueryFilteringTests
    {
        [Fact]
        public void ApplyQuery_OrderIntValueByAsc_IntValueOrderedByAsc()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1},
            }.AsQueryable();

            var actual = testObjects.ApplyQuery("$orderBy=IntValue").ToList();
            var expected = testObjects.OrderBy(x => x.IntValue).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_OrderIntValueByDesc_IntValueOrderedByDesc()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 1},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 3},
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$orderBy=IntValue desc")
                .ToList();

            var expected = testObjects
                .OrderByDescending(x => x.IntValue)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_OrderIntValueByAscThenOrderDoubleValueByDesc_IntValueOrderedByAscAndDoubleValueOrderedByAsc()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 4, DoubleValue = 7},
                new TestObject(){IntValue = 3, DoubleValue = 6},
                new TestObject(){IntValue = 2, DoubleValue = 5},
                new TestObject(){IntValue = 2, DoubleValue = 4},
                new TestObject(){IntValue = 2, DoubleValue = 3},
                new TestObject(){IntValue = 2, DoubleValue = 2},
                new TestObject(){IntValue = 1, DoubleValue = 1},
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$orderBy=IntValue, DoubleValue")
                .ToList();

            var expected = testObjects
                .OrderBy(x => x.IntValue)
                .ThenBy(x => x.DoubleValue)
                .ToList();

            Assert.Equal(expected, actual);
        }
    }
}
