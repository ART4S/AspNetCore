using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Registration
{
    /// <summary>
    /// Команда регистрации пользователя
    /// </summary>
    public class RegisterUser : ICommand<Result>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
