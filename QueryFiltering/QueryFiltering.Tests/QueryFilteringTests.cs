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
        public void ApplyQuery_TopOne_ReturnsOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects
                .Take(1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$top=1")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_TopNegativeNumber_ReturnsZeroObjects()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects
                .Take(-1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$top=-1")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_SkipFirstObjectFromThreeObjects_ReturnsTwoRemainingObjects()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects
                .Skip(1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$skip=1")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_SkipNegativeNumber_ReturnsSameObjects()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects
                .Skip(-1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$skip=-1")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_OrderIntValueByAsc_IntValueOrderedByAsc()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1},
            }.AsQueryable();

            var expected = testObjects
                .OrderBy(x => x.IntValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$orderBy=IntValue")
                .ToList();

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

            var expected = testObjects
                .OrderByDescending(x => x.IntValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$orderBy=IntValue desc")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_OrderIntValueByAscThenOrderDoubleValueByDesc_IntValueOrderedByAscAndDoubleValueOrderedByAsc()
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

            var expected = testObjects
                .OrderBy(x => x.IntValue)
                .ThenBy(x => x.DoubleValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$orderBy=IntValue, DoubleValue")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterIntValueEqualsMaxValue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MaxValue},
                new TestObject(){IntValue = 0 },
                new TestObject(){IntValue = int.MinValue},
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.IntValue == int.MaxValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery($"$filter=IntValue eq {int.MaxValue}")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterIntValueEqualsMinValue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MaxValue},
                new TestObject(){IntValue = 0 },
                new TestObject(){IntValue = int.MinValue}, 
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.IntValue == int.MinValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery($"$filter=IntValue eq {int.MinValue}")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterDoubleValueEqualsPositiveNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = 1},
                new TestObject(){DoubleValue = 0 },
                new TestObject(){DoubleValue = -1}, 
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.DoubleValue == 1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=DoubleValue eq 1.00d")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterDoubleValueEqualsNegativeNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = 1},
                new TestObject(){DoubleValue = 0 },
                new TestObject(){DoubleValue = -1},
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.DoubleValue == -1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=DoubleValue eq -1.00d")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterBoolValueEqualsTrue_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = true},
                new TestObject(){BoolValue = false },
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.BoolValue)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=BoolValue eq true")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterBoolValueEqualsFalse_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = true},
                new TestObject(){BoolValue = false },
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.BoolValue == false)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=BoolValue eq false")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterDecimalValueEqualsPositiveNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = 1},
                new TestObject(){DecimalValue = 0},
                new TestObject(){DecimalValue = -1},
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.DecimalValue == 1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=DecimalValue eq 1m")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterDecimalValueEqualsNegativeNumber_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = 1},
                new TestObject(){DecimalValue = 0},
                new TestObject(){DecimalValue = -1},
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.DecimalValue == -1)
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=DecimalValue eq -1.0m")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterStringValueEqualsNotEmptyString_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "validString"},
                new TestObject(){StringValue = "invalidString"}
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.StringValue == "validString")
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=StringValue eq 'validString'")
                .ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyQuery_FilterStringValueEqualsEmptyString_FilteredOneObject()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = ""},
                new TestObject(){StringValue = "invalidString"}
            }.AsQueryable();

            var expected = testObjects
                .Where(x => x.StringValue == "")
                .ToList();

            var actual = testObjects
                .ApplyQuery("$filter=StringValue eq ''")
                .ToList();

            Assert.Equal(expected, actual);
        }
    }
}
