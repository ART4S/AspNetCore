﻿using System.Security.Claims;
using Web.Infrastructure.Failures;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Authentication.Login
{
    /// <summary>
    /// Команда входа в систему
    /// </summary>
    public class LoginCommand : IRequest<Result<Claim[], Fail>>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}