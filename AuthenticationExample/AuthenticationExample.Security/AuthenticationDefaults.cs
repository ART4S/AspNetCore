using System;

namespace AuthenticationExample.Security
{
    internal static class AuthenticationDefaults
    {
        public static string UserProtectorName = "userProtector";

        public static TimeSpan ExpirationTime = TimeSpan.FromMinutes(20);
    }
}
