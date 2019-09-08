using System;

namespace WebFeatures.Common.Time
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
