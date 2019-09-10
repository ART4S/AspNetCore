using System;

namespace WebFeatures.Common.Time
{
    public class ServerTime : IServerTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
