using QueryFiltering.Tests.Model;
using System.Linq;
using Xunit;

namespace QueryFiltering.Tests
{
    public class QueryFilteringTests
    {
        [Fact]
        public void ApplyQuery_EmptyQuery_ReturnsSameQueryable()
        {
            var testObjects = new[]
            {
                new TestObject()
            }.AsQueryable();

            var actual = testObjects.ApplyQuery("");
            var expected = testObjects;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByAsc_IntValueOrderedByAsc()
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
        public void OrderBy_IntValueByDesc_IntValueOrderedByDesc()
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
        public void OrderBy_IntValueByAscThenOrderDoubleValueByDesc_IntValueOrderedByAscAndDoubleValueOrderedByAsc()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 4, DoubleValue = 1},
                new TestObject(){IntValue = 3, DoubleValue = 6},
                new TestObject(){IntValue = 2, DoubleValue = 4},
                new TestObject(){IntValue = 2, DoubleValue = 3},
                new TestObject(){IntValue = 2, DoubleValue = 5},
                new TestObject(){IntValue = 2, DoubleValue = 2},
                new TestObject(){IntValue = 1, DoubleValue = 7},
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

        [Fact]
        public void Filter_IntValueEqualsMaxValue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MaxValue},
                new TestObject(){IntValue = 0 },
                new TestObject(){IntValue = int.MinValue},
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=IntValue eq 2147483647")
                .ToList();

            var expected = testObjects
                .Where(x => x.IntValue == int.MaxValue)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_IntValueEqualsMinValue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MaxValue},
                new TestObject(){IntValue = 0 },
                new TestObject(){IntValue = int.MinValue}, 
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=IntValue eq -2147483648")
                .ToList();

            var expected = testObjects
                .Where(x => x.IntValue == int.MinValue)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DoubleValueEqualsPositiveNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = 1},
                new TestObject(){DoubleValue = 0 },
                new TestObject(){DoubleValue = -1}, 
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=DoubleValue eq 1.00d")
                .ToList();

            var expected = testObjects
                .Where(x => x.DoubleValue == 1)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DoubleValueEqualsNegativeNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = 1},
                new TestObject(){DoubleValue = 0 },
                new TestObject(){DoubleValue = -1},
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=DoubleValue eq -1.00d")
                .ToList();

            var expected = testObjects
                .Where(x => x.DoubleValue == -1)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_BoolValueEqualsTrue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = true},
                new TestObject(){BoolValue = false },
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=BoolValue eq true")
                .ToList();

            var expected = testObjects
                .Where(x => x.BoolValue)
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_BoolValueEqualsFalse_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = true},
                new TestObject(){BoolValue = false },
            }.AsQueryable();

            var actual = testObjects
                .ApplyQuery("$filter=BoolValue eq false")
                .ToList();

            var expected = testObjects
                .Where(x => x.BoolValue == false)
                .ToList();

            Assert.Equal(expected, actual);
        }
    }
}
