using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using System.Security.Claims;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Authentication.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<Claim[], Fail>>
    {
        private readonly IAppContext _context;
        private readonly IDataProtector _protector;

        public LoginCommandHandler(IDataProtectionProvider protectionProvider, IAppContext context)
        {
            _context = context;
            _protector = protectionProvider.CreateProtector("UserPassword");
        }

        public Result<Claim[], Fail> Handle(LoginCommand input)
        {
            var user = _context.Set<User>().FirstOrDefault(x => x.Name == input.Name);
            if (user == null)
            {
                return Result<Claim[], Fail>.Fail(ValidationErrorMessages.InvalidLoginOrPassword);
            }

            var pass = _protector.Unprotect(user.PasswordHash);
            if (pass != input.Password)
            {
                return Result<Claim[], Fail>.Fail(ValidationErrorMessages.InvalidLoginOrPassword);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            return Result<Claim[], Fail>.Success(claims);
        }
    }
}
