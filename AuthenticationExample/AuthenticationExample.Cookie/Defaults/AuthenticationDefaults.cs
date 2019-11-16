using System;

namespace AuthenticationExample.Cookie.Defaults
{
    internal static class AuthenticationDefaults
    {
        public static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(20);

        public static readonly string Issuer = "https://localhost:5001/";

        public static readonly string Audience = "https://localhost:5001/";
    }
}
