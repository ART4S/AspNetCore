using System.Security.Claims;
using Web.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    public class Login : ICommand<Result<Claim[], string>>
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
