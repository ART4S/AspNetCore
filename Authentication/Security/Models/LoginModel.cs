using System.ComponentModel.DataAnnotations;

namespace Security.Models
{
    /// <summary>
    /// Форма для аутентификации
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
