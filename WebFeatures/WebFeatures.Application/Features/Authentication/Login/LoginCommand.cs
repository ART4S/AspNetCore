using System.Security.Claims;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Authentication.Login
{
    public class LoginCommand : ICommand<Claim[]>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
