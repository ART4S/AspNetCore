using System.Security.Claims;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Authentication.Login
{
    public class LoginCommand : ICommand<Result<Claim[], Fail>>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
