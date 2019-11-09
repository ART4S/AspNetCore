namespace AuthenticationExample.Security.Models
{
    /// <summary>
    /// Форма для аутентификации
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
