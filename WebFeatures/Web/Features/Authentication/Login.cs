using System.Security.Claims;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    /// <summary>
    /// Команда входа в систему
    /// </summary>
    public class Login : ICommand<Result<Claim[], string>>
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
