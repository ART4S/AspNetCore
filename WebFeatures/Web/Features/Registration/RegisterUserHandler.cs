using AutoMapper;
using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Registration
{
    public class RegisterUserHandler : IHandler<RegisterUser, Result>
    {
        private readonly IDataProtector _protector;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IDataProtectionProvider protectionProvider, DbContext dbContext, IMapper mapper)
        {
            _protector = protectionProvider.CreateProtector("UserPassword");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Result Handle(RegisterUser input)
        {
            var alreadyExists = _dbContext.Set<User>().Any(x => x.Name == input.Name);
            if (alreadyExists)
                return Result.Fail("Пользователь с данным именем уже присутствует в системе");

            var user = _mapper.Map<User>(input);
            user.PasswordHash = _protector.Protect(input.Password);

            _dbContext.Add(user);

            return Result.Success();
        }
    }
}
