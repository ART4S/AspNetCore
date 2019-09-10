using System;

namespace WebFeatures.Common.Time
{
    /// <summary>
    /// Время на сервере
    /// </summary>
    public interface IServerTime
    {
        DateTime Now { get; }
    }
}
