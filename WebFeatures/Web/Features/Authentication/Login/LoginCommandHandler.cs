using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Web.Infrastructure.Failures;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Authentication.Login
{
    /// <summary>
    /// Вход в систему
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<Claim[], Fail>>
    {
        private readonly DbContext _dbContext;
        private readonly IDataProtector _protector;

        /// <inheritdoc />
        public LoginCommandHandler(IDataProtectionProvider protectionProvider, DbContext dbContext)
        {
            _dbContext = dbContext;
            _protector = protectionProvider.CreateProtector("UserPassword");
        }

        /// <inheritdoc />
        public Result<Claim[], Fail> Handle(LoginCommand input)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(x => x.Name == input.Name);
            if (user == null)
                return Fail();

            var pass = _protector.Unprotect(user.PasswordHash);
            if (pass != input.Password)
                return Fail();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            return Result<Claim[], Fail>.Success(claims);
        }

        private Result<Claim[], Fail> Fail()
        {
            return Result<Claim[], Fail>.Fail("Неверный логин или пароль");
        }
    }
}
