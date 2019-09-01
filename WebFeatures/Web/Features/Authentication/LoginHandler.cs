using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    /// <summary>
    /// Вход в систему
    /// </summary>
    public class LoginHandler : ICommandHandler<Login, Result<Claim[], string>>
    {
        private readonly DbContext _dbContext;
        private readonly IDataProtector _protector;

        /// <inheritdoc />
        public LoginHandler(IDataProtectionProvider protectionProvider, DbContext dbContext)
        {
            _dbContext = dbContext;
            _protector = protectionProvider.CreateProtector("UserPassword");
        }

        /// <inheritdoc />
        public Result<Claim[], string> Handle(Login input)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(x => x.Name == input.Name);
            if (user == null)
                return ErrorResult();

            var pass = _protector.Unprotect(user.PasswordHash);
            if (pass != input.Password)
                return ErrorResult();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            return Result<Claim[], string>.Success(claims);
        }

        private Result<Claim[], string> ErrorResult()
        {
            return Result<Claim[], string>.Fail("Неверный логин или пароль");
        }
    }
}
