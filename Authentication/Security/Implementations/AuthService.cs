using AutoMapper;
using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Security.Dto;
using Security.Interfaces;
using Security.Models;
using Security.Results;
using System;
using System.Linq;

namespace Security.Implementations
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public AuthService(
            DataContext context,
            IPasswordHasher<Account> passwordHasher,
            IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public AuthenticationResult RegisterAccount(RegisterModel registerModel)
        {
            if (_context.Accounts.Any(x => x.Name == registerModel.Login))
            {
                return AuthenticationResult.Fail("Логин уже существует");
            }

            var accountEntity = _mapper.Map<Account>(registerModel);
            accountEntity.PasswordHash = _passwordHasher.HashPassword(accountEntity, registerModel.Password);

            _context.Accounts.Add(accountEntity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return AuthenticationResult.Fail("Ошибка при попытке создания аккаунта", ex);
            }

            var account = _mapper.Map<AccountDto>(accountEntity);
            return AuthenticationResult.Success(account);
        }

        /// <inheritdoc />
        public AuthenticationResult GetAccount(LoginModel loginModel)
        {
            var accountEntity = _context.Accounts.FirstOrDefault(x => x.Name == loginModel.Login);

            if (accountEntity == null)
            {
                return AuthenticationResult.Fail("Неверный логин или пароль");
            }

            var isValid =
                _passwordHasher.VerifyHashedPassword(accountEntity, accountEntity.PasswordHash, loginModel.Password) ==
                PasswordVerificationResult.Success;

            if (!isValid)
            {
                return AuthenticationResult.Fail("Неверный логин или пароль");
            }

            var account = _mapper.Map<AccountDto>(accountEntity);
            return AuthenticationResult.Success(account);
        }
    }
}
