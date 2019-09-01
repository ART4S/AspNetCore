using AutoMapper;
using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Registration
{
    /// <summary>
    /// Обработчик команды регистрации пользователя
    /// </summary>
    public class RegisterUserCommandHandler : IHandler<RegisterUserCommand, Result>
    {
        private readonly IDataProtector _protector;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public RegisterUserCommandHandler(IDataProtectionProvider protectionProvider, DbContext dbContext, IMapper mapper)
        {
            _protector = protectionProvider.CreateProtector("UserPassword");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Result Handle(RegisterUserCommand input)
        {
            var user = _mapper.Map<User>(input);
            user.PasswordHash = _protector.Protect(input.Password);

            _dbContext.Add(user);

            return Result.Success();
        }
    }
}
