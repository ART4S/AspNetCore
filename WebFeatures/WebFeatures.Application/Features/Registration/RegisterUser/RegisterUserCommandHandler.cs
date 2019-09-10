using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    /// <summary>
    /// Обработчик команды регистрации пользователя
    /// </summary>
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
    {
        private readonly IDataProtector _protector;
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IDataProtectionProvider protectionProvider, IAppContext context, IMapper mapper)
        {
            _protector = protectionProvider.CreateProtector("UserPassword");
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Result Handle(RegisterUserCommand input)
        {
            var user = _mapper.Map<User>(input);
            user.PasswordHash = _protector.Protect(input.Password);

            _context.Set<User>().Add(user);

            return Result.Success();
        }
    }
}
