using System;

namespace WebFeatures.Common.Time
{
    internal class AppDateTimeDefault : AppDateTime
    {
        public override DateTime Now => DateTime.UtcNow;
    }
}
