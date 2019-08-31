using System.Security.Claims;
using Web.Attributes;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    public class Login : ICommand<Result<Claim[], string>>
    {
        [Required]
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
