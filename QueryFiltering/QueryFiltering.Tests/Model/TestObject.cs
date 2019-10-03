using System;

namespace QueryFiltering.Tests.Model
{
    internal class TestObject
    {
        public int IntValue { get; set; }

        public double DoubleValue { get; set; }

        public decimal DecimalValue { get; set; }

        public bool BoolValue { get; set; }

        public string StringValue { get; set; }

        public Guid GuidValue { get; set; }

        public int? NullableIntValue { get; set; }
    }
}
