using QueryFiltering.Tests.Model;
using System;
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

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Top_Count_ReturnsOne(int count)
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Take(count).ToList();
            var actual = testObjects.ApplyQuery($"$top={count}").ToList();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Skip

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Skip_Count_ReturnsOne(int count)
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Skip(count).ToList();
            var actual = testObjects.ApplyQuery($"$skip={count}").ToList();

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

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Filter_IntValueEqualsValue_ReturnsFilteredOne(int value)
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.IntValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=IntValue eq {value}").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void Filter_LongValueEqualsValue_ReturnsFilteredOne(long value)
        {
            var testObjects = new[]
            {
                new TestObject(){LongValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.LongValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=LongValue eq {value}l").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_DoubleValueEqualsValue_ReturnsFilteredOne(int value)
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DoubleValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=DoubleValue eq {value}d").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Filter_BoolValueEqualsValue_ReturnsFilteredOne(bool value)
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = value},
                new TestObject(){BoolValue = !value}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.BoolValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=BoolValue eq {value.ToString().ToLower()}").ToList();

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_DecimalValueEqualsValue_ReturnsFilteredOne(decimal value)
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DecimalValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=DecimalValue eq {value}m").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_FloatValueEqualsValue_ReturnsFilteredOne(float value)
        {
            var testObjects = new[]
            {
                new TestObject(){FloatValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.FloatValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=FloatValue eq {value}m").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2ec37a3c-1529-4298-a1da-30472b68e6a5")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Filter_GuidValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testGuid = Guid.Parse(value);
            var testObjects = new[]
            {
                new TestObject(){GuidValue = testGuid},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.GuidValue == testGuid).ToList();
            var actual = testObjects.ApplyQuery($"$filter=GuidValue eq {testGuid}").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2000-01-01")]
        [InlineData("2000-01-01T12:59")]
        public void Filter_DateTimeValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testDateTime = DateTime.Parse(value);
            var testObjects = new[]
            {
                new TestObject(){DateTimeValue = testDateTime},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DateTimeValue == testDateTime).ToList();
            var actual = testObjects.ApplyQuery($"$filter=DateTimeValue eq datetime'{value}'").ToList();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("notEmptyString")]
        [InlineData("")]
        public void Filter_StringValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=StringValue eq '{value}'").ToList();

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

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void Filter_NullableIntValueEqualsValue_ReturnsFilteredOne(int? value)
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == value).ToList();
            var actual = testObjects.ApplyQuery($"$filter=NullableIntValue eq {value}").ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_NullableIntValueEqualsNull_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = null},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == null).ToList();
            var actual = testObjects.ApplyQuery("$filter=NullableIntValue eq null").ToList();

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
        public void Filter_InnerObjectIntValueEqualsValue_ReturnsFilteredOne()
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
