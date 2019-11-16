using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationExample.Token.Defaults
{
    internal static class AuthenticationDefaults
    {
        public static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(20);

        public static readonly SecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xXxSuperSecurityKeyxXx"));

        public static readonly string Issuer = "https://localhost:5001/";

        public static readonly string Audience = "https://localhost:5001/";
    }
}
