﻿using AutoMapper;
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
    public class RegisterUserHandler : IHandler<RegisterUser, Result>
    {
        private readonly IDataProtector _protector;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public RegisterUserHandler(IDataProtectionProvider protectionProvider, DbContext dbContext, IMapper mapper)
        {
            _protector = protectionProvider.CreateProtector("UserPassword");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Result Handle(RegisterUser input)
        {
            var user = _mapper.Map<User>(input);
            user.PasswordHash = _protector.Protect(input.Password);

            _dbContext.Add(user);

            return Result.Success();
        }
    }
}
