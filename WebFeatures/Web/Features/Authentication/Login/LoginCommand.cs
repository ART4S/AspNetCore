using System.Security.Claims;
using Web.Infrastructure.Decorators.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Features.Authentication.Login
{
    /// <summary>
    /// Команда входа в систему
    /// </summary>
    public class LoginCommand : ICommand<Result<Claim[], string>>
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
