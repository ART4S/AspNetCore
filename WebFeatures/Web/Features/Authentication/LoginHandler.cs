using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Web.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    public class LoginHandler : ICommandHandler<Login, Result<Claim[], string>>
    {
        private readonly DbContext _dbContext;
        private readonly IDataProtector _protector;

        public LoginHandler(IDataProtectionProvider protectionProvider, DbContext dbContext)
        {
            _dbContext = dbContext;
            _protector = protectionProvider.CreateProtector("UserPassword");
        }

        public Result<Claim[], string> Handle(Login input)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(x => x.Name == input.Name);
            if (user == null)
                return ErrorResult();

            var inputHash = _protector.Protect(input.Password);
            if (user.PasswordHash != inputHash)
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
