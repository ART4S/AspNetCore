using System;
using System.Collections.Generic;
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

        #region Top

        [Fact]
        public void Top_1_ReturnsOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Take(1).ToList();
            var actual = testObjects.ApplyQuery("$top=1").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Top_NegativeNumber_ReturnsZero()
        {
            var testObjects = new[]
            {
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Take(-1).ToList();
            var actual = testObjects.ApplyQuery("$top=-1").ToList();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Skip

        [Fact]
        public void Skip_1_ReturnsOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Skip(1).ToList();
            var actual = testObjects.ApplyQuery("$skip=1").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Skip_NegativeNumber_ReturnsSameObjects()
        {
            var testObjects = new[]
            {
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Skip(-1).ToList();
            var actual = testObjects.ApplyQuery("$skip=-1").ToList();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region OrderBy

        [Fact]
        public void OrderBy_IntValueByAsc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1},
            }.AsQueryable();

            var expected = testObjects.OrderBy(x => x.IntValue).ToList();
            var actual = testObjects.ApplyQuery("$orderBy=IntValue").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByDesc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 1},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 3},
            }.AsQueryable();

            var expected = testObjects.OrderByDescending(x => x.IntValue).ToList();
            var actual = testObjects.ApplyQuery("$orderBy=IntValue desc").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByAscThenDoubleValueByDesc_ReturnsOrdered()
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

        #endregion

        #region Filter

        [Fact]
        public void Filter_IntValueEqualsMaxValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MaxValue},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.IntValue == int.MaxValue).ToList();
            var actual = testObjects.ApplyQuery($"$filter=IntValue eq {int.MaxValue}").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_IntValueEqualsMinValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = int.MinValue},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.IntValue == int.MinValue).ToList();
            var actual = testObjects.ApplyQuery($"$filter=IntValue eq {int.MinValue}").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DoubleValueEqualsPositiveNumber_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = 1},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DoubleValue == 1).ToList();
            var actual = testObjects.ApplyQuery("$filter=DoubleValue eq 1.00d").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DoubleValueEqualsNegativeNumber_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = -1},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DoubleValue == -1).ToList();
            var actual = testObjects.ApplyQuery("$filter=DoubleValue eq -1.00d").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_BoolValueEqualsTrue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = true},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.BoolValue).ToList();
            var actual = testObjects.ApplyQuery("$filter=BoolValue eq true").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_BoolValueEqualsFalse_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(){BoolValue = true}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.BoolValue == false).ToList();
            var actual = testObjects.ApplyQuery("$filter=BoolValue eq false").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DecimalValueEqualsPositiveNumber_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = 1},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DecimalValue == 1).ToList();
            var actual = testObjects.ApplyQuery("$filter=DecimalValue eq 1m").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DecimalValueEqualsNegativeNumber_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = -1},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DecimalValue == -1).ToList();
            var actual = testObjects.ApplyQuery("$filter=DecimalValue eq -1.0m").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEqualsNotEmptyString_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == "match").ToList();
            var actual = testObjects.ApplyQuery("$filter=StringValue eq 'match'").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEqualsEmptyString_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = string.Empty},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == string.Empty).ToList();
            var actual = testObjects.ApplyQuery("$filter=StringValue eq ''").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueStartsWithSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.StartsWith("match")).ToList();
            var actual = testObjects.ApplyQuery("$filter=startswith(StringValue, 'match') eq true").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEndsWithSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.EndsWith("match")).ToList();
            var actual = testObjects.ApplyQuery("$filter=endswith(StringValue, 'match') eq true").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_ToUpperStringValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.ToUpper() == "MATCH").ToList();
            var actual = testObjects.ApplyQuery("$filter=toupper(StringValue) eq 'MATCH'").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_ToLowerStringValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "MATCH"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.ToLower() == "match").ToList();
            var actual = testObjects.ApplyQuery("$filter=tolower(StringValue) eq 'match'").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEqualsNull_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == null).ToList();
            var actual = testObjects.ApplyQuery("$filter=StringValue eq null").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueNotEqualsSomeString_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = "notMatch"},
            }.AsQueryable();

            var expected = testObjects.Where(x => !(x.StringValue == "notMatch")).ToList();
            var actual = testObjects.ApplyQuery("$filter=not StringValue eq 'notMatch'").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_GuidValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testGuid = Guid.NewGuid();
            var testObjects = new[]
            {
                new TestObject(){GuidValue = testGuid},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.GuidValue == testGuid).ToList();
            var actual = testObjects.ApplyQuery($"$filter=GuidValue eq {testGuid}").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_NullableIntValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = 1},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == 1).ToList();
            var actual = testObjects.ApplyQuery("$filter=NullableIntValue eq 1").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_NullableIntValueEqualsNull_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = null},
                new TestObject(){NullableIntValue = 0}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == null).ToList();
            var actual = testObjects.ApplyQuery("$filter=NullableIntValue eq null").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_DateTimeValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testDateTime = new DateTime(2000, 1, 1);
            var testObjects = new[]
            {
                new TestObject(){DateTimeValue = testDateTime},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DateTimeValue == testDateTime).ToList();
            var actual = testObjects.ApplyQuery("$filter=DateTimeValue eq datetime'2000-01-01'").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_InnerObjectIntValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.InnerObject != null && x.InnerObject.IntValue == 1).ToList();
            var actual = testObjects.ApplyQuery("$filter=InnerObject ne null and InnerObject.IntValue eq 1").ToList();

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
