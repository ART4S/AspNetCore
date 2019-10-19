using System;

namespace WebFeatures.Common.Time
{
    public abstract class AppDateTime
    {
        public static AppDateTime Instance
        {
            get => _instance ?? (_instance = new AppDateTimeDefault());
            set => _instance = value;
        }
        private static AppDateTime _instance;

        public abstract DateTime Now { get; }
    }
}
