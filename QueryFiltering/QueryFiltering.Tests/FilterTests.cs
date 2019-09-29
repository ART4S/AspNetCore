using QueryFiltering.Tests.Model;
using QueryFiltering.Visitors;
using System;
using System.Linq.Expressions;
using Xunit;

namespace QueryFiltering.Tests
{
    public class FilterTests
    {
        [Fact]
        public void Filter_IntValueEqualsMaxValue_ExpectedTrue()
        {
            var input = "$filter=IntValue eq 2147483647";
            var testObject = new TestObject() { IntValue = int.MaxValue };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }

        [Fact]
        public void Filter_IntValueEqualsMinValue_ExpectedTrue()
        {
            var input = "$filter=IntValue eq -2147483648";
            var testObject = new TestObject() { IntValue = int.MinValue };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }

        [Fact]
        public void Filter_DoubleValueEqualsPositiveNumber_ExpectedTrue()
        {
            var input = "$filter=DoubleValue eq 1.00d";
            var testObject = new TestObject() { DoubleValue = 1 };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }

        [Fact]
        public void Filter_DoubleValueEqualsNegativeNumber_ExpectedTrue()
        {
            var input = "$filter=DoubleValue eq -1d";
            var testObject = new TestObject() { DoubleValue = -1 };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }

        [Fact]
        public void Filter_BoolValueEqualsTrue_ExpectedTrue()
        {
            var input = "$filter=BoolValue eq true";
            var testObject = new TestObject() { BoolValue = true };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }

        [Fact]
        public void Filter_BoolValueEqualsFalse_ExpectedTrue()
        {
            var input = "$filter=BoolValue eq false";
            var testObject = new TestObject() { BoolValue = false };

            var visitor = new FilterVisitor(typeof(TestObject));

            var parser = TestHelper.CreateParser(input);
            var tree = parser.filter().Accept(visitor);
            var expression = (Expression<Func<TestObject, bool>>)tree.Build();
            var func = expression.Compile();

            var result = func(testObject);

            Assert.True(result);
        }
    }
}
