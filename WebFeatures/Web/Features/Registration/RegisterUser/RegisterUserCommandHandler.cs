using AutoMapper;
using Entities.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Registration.RegisterUser
{
    /// <summary>
    /// Обработчик команды регистрации пользователя
    /// </summary>
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
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
        public Result Handle(RegisterUserCommand request)
        {
            var user = _mapper.Map<User>(request);
            user.PasswordHash = _protector.Protect(request.Password);

            _dbContext.Add(user);

            return Result.Success();
        }
    }
}
