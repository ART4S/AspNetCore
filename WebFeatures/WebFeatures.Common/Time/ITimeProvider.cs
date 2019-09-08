namespace WebFeatures.Common.Time
{
    /// <summary>
    /// Время на сервере
    /// </summary>
    public interface ITimeProvider
    {
        System.DateTime Now { get; }
    }
}
