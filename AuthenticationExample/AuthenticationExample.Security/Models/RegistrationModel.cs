namespace AuthenticationExample.Security.Models
{
    /// <summary>
    /// Форма для регистрации
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
