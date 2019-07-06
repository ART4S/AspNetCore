using Security.Models;
using Security.Results;

namespace Security.Interfaces
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация аккаунта
        /// </summary>
        /// <param name="registerModel">Форма для регистрации</param>
        AuthenticationResult RegisterAccount(RegisterModel registerModel);

        /// <summary>
        /// Верифицировать аккаунт
        /// </summary>
        /// <param name="loginModel">Форма для логина</param>
        AuthenticationResult GetAccount(LoginModel loginModel);
    }
}
